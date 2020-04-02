using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public interface IFriendDataHandler
    {
        bool createFriend();
        bool updateFriend();
        bool deleteFriend();
        List<Friend> getAllFriends(int userId1);
    }
}
