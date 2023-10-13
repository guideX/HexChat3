using HexChat.Business.EventArgs;
using HexChat.Business.Messages;
using System.Reflection;
namespace HexChat.Business.Business {
    /// <summary>
    /// Ctcp Command Business
    /// </summary>
    public class CtcpCommandBusiness {
        /// <summary>
        /// ACTION
        /// </summary>
        public const string ACTION = nameof(ACTION);
        /// <summary>
        /// CLIENT INFO
        /// </summary>
        public const string CLIENTINFO = nameof(CLIENTINFO);
        /// <summary>
        /// ERR MSG
        /// </summary>
        public const string ERRMSG = nameof(ERRMSG);
        /// <summary>
        /// PING
        /// </summary>
        public const string PING = nameof(PING);
        /// <summary>
        /// TIME
        /// </summary>
        public const string TIME = nameof(TIME);
        /// <summary>
        /// VERSION
        /// </summary>
        public const string VERSION = nameof(VERSION);
        /// <summary>
        /// Handle Ctcp
        /// </summary>
        /// <param name="client"></param>
        /// <param name="ctcp"></param>
        /// <returns></returns>
        internal static Task HandleCtcp(ClientBusiness client, CtcpEventArgs ctcp) {
            switch (ctcp.CtcpCommand.ToUpper()) {
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
        /// <summary>
        /// Client Info Reply
        /// </summary>
        /// <param name="client"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static Task ClientInfoReply(ClientBusiness client, string target) {
            return client.SendAsync(new CtcpReplyMessageBusiness(target, $"{CLIENTINFO} {ACTION} {CLIENTINFO} {PING} {TIME} {VERSION}"));
        }
        /// <summary>
        /// Ping Reply
        /// </summary>
        /// <param name="client"></param>
        /// <param name="target"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Task PingReply(ClientBusiness client, string target, string message) {
            return client.SendAsync(new CtcpReplyMessageBusiness(target, $"{PING} {message}"));
        }
        /// <summary>
        /// Time Reply
        /// </summary>
        /// <param name="client"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static Task TimeReply(ClientBusiness client, string target) {
            return client.SendAsync(new CtcpReplyMessageBusiness(target, $"{TIME} {DateTime.Now:F}"));
        }
        /// <summary>
        /// Version Reply
        /// </summary>
        /// <param name="client"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static Task VersionReply(ClientBusiness client, string target) {
            var version = typeof(ClientBusiness).Assembly
                .GetCustomAttribute<AssemblyFileVersionAttribute>()
                .Version;
            return client.SendAsync(new CtcpReplyMessageBusiness(target, $"{VERSION} HexChat v{version})"));
        }
    }
}