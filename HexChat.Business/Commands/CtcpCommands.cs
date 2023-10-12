using HexChat.Business.Business;
using HexChat.Business.EventArgs;
using HexChat.Business.Messages;
using System.Reflection;
namespace HexChat.Business.Commands {
    public static class CtcpCommands {
        internal const string CtcpDelimiter = "\x01";

        public const string ACTION = nameof(ACTION);

        public const string CLIENTINFO = nameof(CLIENTINFO);

        public const string ERRMSG = nameof(ERRMSG);

        public const string PING = nameof(PING);

        public const string TIME = nameof(TIME);

        public const string VERSION = nameof(VERSION);

        internal static Task HandleCtcp(ClientBusiness client, CtcpEventArgs ctcp) {
            switch (ctcp.CtcpCommand) {
                case ACTION:
                    var msg = "";
                    msg = "2";
                    break;

                case ERRMSG:
                    break;
                case CLIENTINFO:
                    return ClientInfoReply(client, ctcp.From);
                case PING:
                    return PingReply(client, ctcp.From, ctcp.CtcpMessage);
                case TIME:
                    return TimeReply(client, ctcp.From);
                case VERSION:
                    return VersionReply(client, ctcp.From);
            }

            return Task.CompletedTask;
        }

        private static Task ClientInfoReply(ClientBusiness client, string target) {
            return client.SendAsync(new CtcpReplyMessage(target, $"{CLIENTINFO} {ACTION} {CLIENTINFO} {PING} {TIME} {VERSION}"));
        }

        private static Task PingReply(ClientBusiness client, string target, string message) {
            return client.SendAsync(new CtcpReplyMessage(target, $"{PING} {message}"));
        }

        private static Task TimeReply(ClientBusiness client, string target) {
            return client.SendAsync(new CtcpReplyMessage(target, $"{TIME} {DateTime.Now:F}"));
        }

        private static Task VersionReply(ClientBusiness client, string target) {
            var version = typeof(ClientBusiness).Assembly
                .GetCustomAttribute<AssemblyFileVersionAttribute>()
                .Version;
            return client.SendAsync(new CtcpReplyMessage(target, $"{VERSION} nexIRC v{version})"));
        }
    }
}