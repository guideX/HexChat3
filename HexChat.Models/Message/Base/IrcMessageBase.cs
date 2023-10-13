﻿using HexChat.Models.Interfaces;
using System.Text;
namespace HexChat.Business.Messages.Base {
    /// <summary>
    /// Irc Message
    /// </summary>
    public abstract class IRCMessage {
        /// <summary>
        /// IRC Message
        /// </summary>
        public DateTime CreatedDate { get; } = DateTime.Now;
        public override string ToString() {
            return this switch {
                ISplitClientMessage clientMessage => BuildClientMessage(clientMessage),
                IClientMessage clientMessage => BuildClientMessage(clientMessage),
                _ => base.ToString()!,
            };
        }/// <summary>
        /// Build Client Message
        /// </summary>
        /// <param name="clientMessage"></param>
        /// <returns></returns>
        private string BuildClientMessage(ISplitClientMessage clientMessage) {
            var sb = new StringBuilder(1024);
            foreach (var tokens in clientMessage.LineSplitTokens) {
                if (tokens.Length == 0) {
                    continue;
                }
                AppendTokens(sb, tokens);
                sb.Append(Microsoft.VisualBasic.Constants.vbCrLf);
            }
            return sb.ToString().Trim();
        }

        private string BuildClientMessage(IClientMessage clientMessage) {
            var tokens = clientMessage.Tokens.ToArray();

            if (tokens.Length == 0) {
                return string.Empty;
            }

            var sb = new StringBuilder(256);

            AppendTokens(sb, tokens);

            return sb.ToString().Trim();
        }

        private static void AppendTokens(StringBuilder sb, string[] tokens) {
            var lastIndex = tokens.Length - 1;

            try {
                for (int i = 0; i < tokens.Length; i++) {
                    if (i == lastIndex && tokens[i].Contains(' ')) {
                        sb.Append(':');
                    }

                    sb.Append(tokens[i]);

                    if (i < lastIndex) {
                        sb.Append(' ');
                    }
                }
            } catch {

            }
        }
    }
}