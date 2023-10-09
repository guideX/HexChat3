namespace HexChat.Models {
    /// <summary>
    /// IRC Prefix Model
    /// </summary>
    public class IRCPrefixModel {
        #region "public variables"
        /// <summary>
        /// Raw
        /// </summary>
        public string? Raw { get; }
        /// <summary>
        /// From
        /// </summary>
        public string? From { get; }
        /// <summary>
        /// User
        /// </summary>
        public string? User { get; }
        /// <summary>
        /// Host
        /// </summary>
        public string? Host { get; }
        #endregion
        #region "methods"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prefixData"></param>
        public IRCPrefixModel(string prefixData) {
            Raw = prefixData;
            From = prefixData;
            if (prefixData.Contains("@")) {
                var splitedPrefix = prefixData.Split('@');
                From = splitedPrefix[0];
                Host = splitedPrefix[1];
            }
            if (From.Contains("!")) {
                var splittedFrom = From.Split('!');
                From = splittedFrom[0];
                User = splittedFrom[1];
            }
        }
        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Raw!;
        }
        #endregion
    }
}