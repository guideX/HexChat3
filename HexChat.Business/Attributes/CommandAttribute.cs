namespace HexChat.Business.Attributes {
    /// <summary>
    /// Command Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandAttribute : Attribute {
        #region "public variables"
        /// <summary>
        /// Command
        /// </summary>
        public string Command { get; }
        #endregion
        #region "methods"
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CommandAttribute(string command) {
            if (string.IsNullOrWhiteSpace(command)) throw new ArgumentNullException(nameof(command));
            Command = command.ToUpper();
        }
        #endregion
    }
}