﻿using HexChat.Business.Messages.Base;
using HexChat.Constant;
using HexChat.Models.Interfaces;
using HexChat.Models.Message;
using System.Text;
namespace HexChat.Business.Business {
    /// <summary>
    /// PrivMsg Message Business
    /// </summary>
    public class PrivMsgMessageBusiness : IRCMessage, IServerMessage, IClientMessage, ISplitClientMessage {
        /// <summary>
        /// PrivMsg Message Model
        /// </summary>
        public PrivMsgMessageModel Model;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parsedMessage"></param>
        public PrivMsgMessageBusiness(ParsedIRCMessageModel parsedMessage) {
            Model = new PrivMsgMessageModel(parsedMessage);
        }

        public PrivMsgMessageBusiness(string target, string text) {
            Model = new PrivMsgMessageModel(null) {
                To = target,
                Message = !text.Contains(" ") ? $":{text}" : text
            };
            Model.To = target;
            Model.Message = !text.Contains(" ") ? $":{text}" : text;
        }

        public IEnumerable<string> Tokens => Enumerable.Empty<string>();

        public IEnumerable<string[]> LineSplitTokens => BuildTokensFromMessageChunks();

        private IEnumerable<string[]> BuildTokensFromMessageChunks() {
            using var reader = new StringReader(Model.Message);
            string line;
            while ((line = reader.ReadLine()) != null) {
                if (string.IsNullOrWhiteSpace(line)) {
                    continue;
                }

                var utf8Text = Encoding.UTF8.GetBytes(Model.Message);

                var index = 0;
                var size = 0;
                var chunkStart = 0;
                while (index < utf8Text.Length) {
                    if (size >= Constants.MaxMessageByteSize) {
                        var messageChunk = Encoding.UTF8.GetString(utf8Text.Skip(chunkStart).Take(size).ToArray());
                        yield return GetTokens(messageChunk);

                        // prepare for next chunk
                        chunkStart = index;
                        size = 0;
                    }

                    // skip bytes that form a utf-8 character
                    int length = GetUtf8CharLength(utf8Text[index]);
                    index += length;
                    size += length;

                    // last chunk
                    if (index == utf8Text.Length) {
                        var messageChunk = Encoding.UTF8.GetString(utf8Text.Skip(chunkStart).ToArray());
                        yield return GetTokens(messageChunk);
                    }
                }
            }

            int GetUtf8CharLength(byte b) {
                if (b < 0x80) return 1;
                else if ((b & 0xE0) == 0xC0) return 2;
                else if ((b & 0xF0) == 0xE0) return 3;
                else if ((b & 0xF8) == 0xF0) return 4;
                else if ((b & 0xfc) == 0xf8) return 5;
                else return 6;
            }
        }

        private string[] GetTokens(string message) => new[] { "PRIVMSG", Model.To, message };
    }
} 