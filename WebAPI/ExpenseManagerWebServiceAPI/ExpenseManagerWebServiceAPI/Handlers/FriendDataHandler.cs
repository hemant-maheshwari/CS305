using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.ViewModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

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
            return saveFriend("create_friend");
        }

        public bool deleteFriend(int userId2)
        {
            throw new NotImplementedException();
        }

        public List<FriendInfo> getAllFriendInfo(int userId1)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<FriendInfo> friendInfos = new List<FriendInfo>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "get_all_friend_infos";  //CREATE STORE PROCEDURE
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id1", userId1));

                //mySqlCommand.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    FriendInfo friendInfo = new FriendInfo();
                    friendInfo.friendId = Int32.Parse(reader["user_id"].ToString());
                    friendInfo.firstName = reader["first_name"].ToString();
                    friendInfo.lastName = reader["last_name"].ToString();
                    friendInfo.amount = Double.Parse(reader["amount"].ToString());

                    friendInfos.Add(friendInfo); //adding to Vendor List
                }
            }
            catch (Exception ex)
            {
                //Log exception
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return friendInfos;
        }

        public List<Friend> getAllFriends(int userId1)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<Friend> friends = new List<Friend>();
            try {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "get_all_friends";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id1", userId1));

                //mySqlCommand.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Friend friend = new Friend();

                    friend.userId1 = Int32.Parse(reader["user_id1"].ToString());
                    friend.userId2 = Int32.Parse(reader["user_id2"].ToString());
                    friend.amount = Double.Parse(reader["amount"].ToString());

                    friends.Add(friend); //adding to Vendor List
                }
            }
            catch (Exception ex)
            {
                //Log exception
                System.Diagnostics.Debug.WriteLine(ex.Message);                
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return friends;
        }

        public bool updateFriend()
        {
            return saveFriend("update_friend");
        }

        private bool saveFriend(string command) {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();

            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = command;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id1", this.userId1));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id2", this.userId2));
                mySqlCommand.Parameters.Add(new MySqlParameter("_amount", this.amount));
                mySqlCommand.Parameters.Add(new MySqlParameter("_response", 0));
                mySqlCommand.Parameters["_response"].Direction = ParameterDirection.Output;

                mySqlCommand.ExecuteNonQuery();

                var result = mySqlCommand.Parameters["_response"].Value;

                //if result is 1, it means stored procedure ran successfully without any error 
                if (Convert.ToInt32(result) == 1)
                {
                    response = true;
                }
            }
            catch (Exception ex)
            {
                //Log exception

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return response;
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return response;
        }
    }
}
