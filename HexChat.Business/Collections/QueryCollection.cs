﻿using System.Collections.ObjectModel;
using HexChat.Business.Business;
using HexChat.Models.Query;
using HexChat.Models.User;
namespace HexChat.Business.Collections {
    /// <summary>
    /// Query Collection
    /// </summary>
    public class QueryCollection : ObservableCollection<QueryModel> {
        /// <summary>
        /// Get Query
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public QueryModel GetQuery(UserModel user) {
            var query = Items.FirstOrDefault(q => q.User.Nick == user.Nick);
            if (query is null) {
                query = new QueryModel(user);
                ClientBusiness.DispatcherInvoker.Invoke(() => Add(query));
            }
            return query;
        }
    }
}