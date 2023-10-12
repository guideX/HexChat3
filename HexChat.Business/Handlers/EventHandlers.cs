using HexChat.Business.Business;
using HexChat.Models.Message;
namespace HexChat.Business.Handlers {
    /// <summary>
    /// IRC Raw Data Handler
    /// </summary>
    /// <param name="client"></param>
    /// <param name="rawData"></param>
    public delegate void IRCRawDataHandler(ClientBusiness client, string rawData);
    /// <summary>
    /// Parsed IRC Message Handler
    /// </summary>
    /// <param name="client"></param>
    /// <param name="ircMessage"></param>
    public delegate void ParsedIRCMessageHandler(ClientBusiness client, ParsedIRCMessageModel ircMessage);
}
