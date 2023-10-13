using HexChat.Constant;
using HexChat.Models.Channel;
using System.Collections;
namespace HexChat.Business.Comparers {
    /// <summary>
    /// Channel user Comparer
    /// </summary>
    public class ChannelUserComparer : IComparer<ChannelUserModel>, IComparer {
        /// <summary>
        /// Compare
        /// </summary>
        /// <param name="u1"></param>
        /// <param name="u2"></param>
        /// <returns></returns>
        public int Compare(ChannelUserModel? u1, ChannelUserModel? u2) {
            if (u1 == null || u2 == null) return 0;
            if (!string.IsNullOrWhiteSpace(u1.Status) && !string.IsNullOrWhiteSpace(u2.Status)) {
                if (Array.IndexOf(Constants.UserStatuses, u1.Status[0]) < Array.IndexOf(Constants.UserStatuses, u2.Status[0])) return -1;
                if (Array.IndexOf(Constants.UserStatuses, u1.Status[0]) > Array.IndexOf(Constants.UserStatuses, u2.Status[0])) return 1;
                return u1.Nick.CompareTo(u2.Nick);
            }
            if (!string.IsNullOrWhiteSpace(u1.Status)) return -1;
            if (!string.IsNullOrWhiteSpace(u2.Status)) return 1;
            if (u1.Nick != null) return u1.Nick.CompareTo(u2.Nick);
            return 0;
        }
        /// <summary>
        /// Compare
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object? x, object? y) {
            if (x is not ChannelUserModel u1 || y is not ChannelUserModel u2) return 0;
            return Compare(u1, u2);
        }
    }
}