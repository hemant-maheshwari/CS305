using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.ViewModels;
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
        bool deleteFriend(int userId2);
        List<FriendInfo> getAllFriendInfo(int userId1);
        List<Friend> getAllFriends(int userId1);
    }
}
