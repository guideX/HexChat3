namespace HexChat.Business.EventArgs {
    /// <summary>
    /// Data Received Event Args
    /// </summary>
    public class DataReceivedEventArgs : System.EventArgs {
        /// <summary>
        /// Data
        /// </summary>
        public string Data { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public DataReceivedEventArgs(string data) {
            Data = data;
        }
    }
}