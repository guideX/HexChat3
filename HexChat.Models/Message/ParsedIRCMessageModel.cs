using HexChat.Enum;
namespace HexChat.Models.Message {
    /// <summary>
    /// Parsed IRC Message Model
    /// </summary>
    public class ParsedIRCMessageModel {
        #region "public properties"
        /// <summary>
        /// The prefix of the message
        /// </summary>
        public IRCPrefixModel Prefix { get; set; }
        /// <summary>
        /// The command received
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// Raw
        /// </summary>
        public string Raw { get; set; }
        #endregion
        #region "private variables"
        /// <summary>
        /// Trailing Prefix
        /// </summary>
        private readonly static char[] _trailingPrefix = { ' ', ':' };
        /// <summary>
        /// Space
        /// </summary>
        private readonly static char[] _space = { ' ' };

        /// <summary>
        /// Parameters
        /// </summary>
        public string[] Parameters { get; private set; }

        /// <summary>
        /// Trailing
        /// </summary>
        public string Trailing => Parameters != null ? Parameters[Parameters.Length - 1] : string.Empty;

        /// <summary>
        /// An Enum representing the IRC command
        /// </summary>
        public IRCCommandEnum IRCCommand { get; private set; }

        /// <summary>
        /// An Enum representing the IRC numeric reply
        /// </summary>
        public IRCNumericReplyEnum NumericReply { get; private set; }

        /// <summary>
        /// Provides you a way to quickly check if the message is a numeric reply
        /// </summary>
        public bool IsNumeric => NumericReply != IRCNumericReplyEnum.UNKNOWN;
        #endregion
        /// <summary>
        /// Initializes a new instance of ParsedIRCMessage, parsing the raw data
        /// </summary>
        /// <param name="rawData">Raw data to be parsed</param>
        public ParsedIRCMessageModel(string rawData) {
            Raw = rawData;
            Parse(rawData.AsSpan());
            ParseIRCEnums();
        }

        private void ParseIRCEnums() {
            if (string.IsNullOrEmpty(Command)) {
                return;
            }
            if (IsNumericReply(Command)) {
                System.Enum.TryParse(Command, out IRCNumericReplyEnum numericReply);
                NumericReply = numericReply;

                // If numericReply's value is still considered a numeric reply, then it's unknown
                // because at this point it should be something like RPL_WELCOME, or another member of IRCNumericReply
                if (IsNumericReply(numericReply.ToString())) {
                    NumericReply = IRCNumericReplyEnum.UNKNOWN;
                }
            } else if (System.Enum.TryParse(Command, out IRCCommandEnum ircCommand)) {
                IRCCommand = ircCommand;
            }
        }
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="rawData"></param>
        private void Parse(ReadOnlySpan<char> rawData) {
            var trailing = string.Empty;
            var indexOfNextSpace = 0;
            if (RawDataHasPrefix) {
                indexOfNextSpace = rawData.IndexOf(Space);
                var prefixData = rawData.Slice(1, indexOfNextSpace - 1);
                Prefix = new IRCPrefixModel(prefixData.ToString());
                rawData = rawData.Slice(indexOfNextSpace + 1);
            }
            var indexOfTrailingStart = rawData.IndexOf(TrailingPrefix);
            if (indexOfTrailingStart > -1) {
                trailing = rawData.Slice(indexOfTrailingStart + 2).Trim().ToString();
                rawData = rawData.Slice(0, indexOfTrailingStart);
            }
            if (DataDoesNotContainSpaces(rawData)) {
                Command = rawData.ToString();
                if (!string.IsNullOrEmpty(trailing)) {
                    Parameters = new[] { trailing };
                }
                return;
            }
            indexOfNextSpace = rawData.IndexOf(Space);
            Command = rawData.Slice(0, indexOfNextSpace).ToString();
            rawData = rawData.Slice(indexOfNextSpace + 1);
            var parameters = new List<string>();
            while ((indexOfNextSpace = rawData.IndexOf(Space)) > -1) {
                parameters.Add(rawData.Slice(0, indexOfNextSpace).ToString());
                rawData = rawData.Slice(indexOfNextSpace + 1);
            }
            if (!rawData.IsWhiteSpace()) {
                parameters.Add(rawData.ToString());
            }
            if (!string.IsNullOrEmpty(trailing)) {
                parameters.Add(trailing);
            }
            Parameters = parameters.ToArray();
        }
        /// <summary>
        /// Raw Data Has Prefix
        /// </summary>
        private bool RawDataHasPrefix => Raw.StartsWith(":");
        /// <summary>
        /// Data Does Not Contain Spaces
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DataDoesNotContainSpaces(ReadOnlySpan<char> data) => !data.Contains(Space, StringComparison.InvariantCultureIgnoreCase);
        /// <summary>
        /// Is Numeric Reply
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool IsNumericReply(string command) => command.Length == 3 && command.ToCharArray().All(char.IsDigit);
        /// <summary>
        /// Returns a string that represents the parsed IRC message
        /// </summary>
        /// <returns>String that represents the parsed IRC message</returns>
        public override string ToString() {
            var paramsDescription = Parameters != null ? "{ " + string.Join(", ", Parameters) + " }" : string.Empty;
            return $"Prefix: {Prefix}, Command: {Command}, Params: {paramsDescription}, Trailing: {Trailing}";
        }
    }
}