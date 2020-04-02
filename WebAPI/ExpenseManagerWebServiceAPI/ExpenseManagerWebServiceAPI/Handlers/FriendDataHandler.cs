using ExpenseManager.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public class FriendDataHandler : Friend, IFriendDataHandler
    {
        private readonly IConfiguration config;

        public FriendDataHandler(IConfiguration config) {
            this.config = config;
        }
        public bool createFriend()
        {
            throw new NotImplementedException();
        }

        public bool deleteFriend()
        {
            throw new NotImplementedException();
        }

        public List<Friend> getAllFriends(int userId1)
        {
            throw new NotImplementedException();
        }

        public bool updateFriend()
        {
            throw new NotImplementedException();
        }
    }
}
