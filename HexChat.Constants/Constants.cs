﻿namespace HexChat.Constant {
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants {
        /// <summary>
        /// User Statuses
        /// </summary>
        public static char[] UserStatuses = new[] { '~', '&', '@', '%', '+' };
        /// <summary>
        /// Ctcp Delimiter
        /// </summary>
        public static string CtcpDelimiter = "\x01";
        /// <summary>
        /// Space
        /// </summary>
        public static string Space = " ";
        /// <summary>
        /// Max Message Byte Size
        /// </summary>
        public const int MaxMessageByteSize = 400;
    }
}