using ExpenseManager.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public class UserDataHandler : User, IUserDataHandler
    {
        private readonly IConfiguration config;
        public UserDataHandler(IConfiguration config) {
            this.config = config;
        }

        public bool checkUsername(string username)
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();

            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "check_username";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_username", username));
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
                //LogHandler.LogError("UserDataHandler.createUser()", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return response;
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return response;
        }

        public bool createUser()
        {
            return saveUser("create_user");
        }

        public User findUser()
        {
            User user = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "find_user";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_username", this.username));
                mySqlCommand.Parameters.Add(new MySqlParameter("_password", this.password));

                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    user = getUserFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return user;
        }

        public User getUser(int userId)
        {
            User user = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try {
                conn.Open();
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "get_user";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));

                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    user = getUserFromReader(reader);
                }
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return user;
        }

        public User validateUser(string username)
        {
            User user = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "get_user_username";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_username", username));

                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    user = getUserFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return user;
        }

        public bool updateUser()
        {
            return saveUser("update_user");
        }

        private bool saveUser(string command) {
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

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_username", this.username));
                mySqlCommand.Parameters.Add(new MySqlParameter("_password", this.password));
                mySqlCommand.Parameters.Add(new MySqlParameter("_first_name", this.firstName));
                mySqlCommand.Parameters.Add(new MySqlParameter("_last_name", this.lastName));
                mySqlCommand.Parameters.Add(new MySqlParameter("_email", this.email));
                mySqlCommand.Parameters.Add(new MySqlParameter("_phone", this.phone));
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return response;
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return response;
        }

        private User getUserFromReader(MySqlDataReader reader) {
            User user = new User();
            user.userId = Int32.Parse(reader["user_id"].ToString());
            user.email = reader["email"].ToString();
            user.firstName = reader["first_name"].ToString();
            user.lastName = reader["last_name"].ToString();
            user.phone = reader["phone"].ToString();
            user.username = reader["username"].ToString();
            user.password = reader["password"].ToString();
            return user;
        }

    }
}
