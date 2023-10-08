using HexChat.Models.Channel;
using System.Collections;
using System.Threading.Channels;
#pragma warning disable CRRSP08 // A misspelled word has been found
namespace HexChat.Business.Comparers {
#pragma warning restore CRRSP08 // A misspelled word has been found
    /// <summary>
    /// Channel user Comparer
    /// </summary>
    public class ChannelUserComparer : IComparer<ChannelUserModel>, IComparer {
        public int Compare(ChannelUserModel u1, ChannelUserModel u2) {
            if (!string.IsNullOrWhiteSpace(u1.Status) && !string.IsNullOrWhiteSpace(u2.Status)) {
                var statuses = ChannelModel.UserStatuses;
                var s1 = u1.Status[0];
                var s2 = u2.Status[0];
                if (Array.IndexOf(statuses, s1) < Array.IndexOf(statuses, s2))
                    return -1;
                if (Array.IndexOf(statuses, s1) > Array.IndexOf(statuses, s2))
                    return 1;
                return u1.Nick.CompareTo(u2.Nick);
            }

            if (!string.IsNullOrWhiteSpace(u1.Status))
                return -1;

            if (!string.IsNullOrWhiteSpace(u2.Status))
                return 1;

            return u1.Nick.CompareTo(u2.Nick);
        }

        public int Compare(object x, object y) {
            if (x is not ChannelUser u1 || y is not ChannelUser u2)
                return 0;

            return Compare(u1, u2);
        }
    }
}