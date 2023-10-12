﻿using System;
using HexChat.Business.Business;
using HexChat.Business.Commands;
using HexChat.Models;

namespace HexChat.Business.EventArgs {
    public delegate void CtcpHandler(ClientBusiness client, CtcpEventArgs ctcpEventArgs);
    public class CtcpEventArgs : System.EventArgs {
        public string From { get; }
        public IRCPrefixModel Prefix { get; }
        public string To { get; }
        public string Message { get; }

#pragma warning disable CRRSP08 // A misspelled word has been found
        public string CtcpCommand { get; }
#pragma warning restore CRRSP08 // A misspelled word has been found
#pragma warning disable CRRSP08 // A misspelled word has been found
        public string CtcpMessage { get; }
#pragma warning restore CRRSP08 // A misspelled word has been found

        internal CtcpEventArgs(PrivMsgMessage privMsgMessage) {
            From = privMsgMessage.From;
            Prefix = privMsgMessage.Prefix;
            To = privMsgMessage.To;
            Message = privMsgMessage.Message;

            var ctcpMessage = Message.Replace(CtcpCommands.CtcpDelimiter, string.Empty);
            if (ctcpMessage.Contains(" ")) {
                var startIndex = ctcpMessage.IndexOf(' ');
                CtcpCommand = ctcpMessage.Remove(startIndex);
                CtcpMessage = ctcpMessage.Substring(startIndex + 1);
                return;
            }
            CtcpCommand = ctcpMessage;
        }
    }
}