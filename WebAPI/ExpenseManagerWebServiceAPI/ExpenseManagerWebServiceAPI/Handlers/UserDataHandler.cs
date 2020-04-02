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
        public int createUser()
        {
            int userId = 0;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();

            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "create_user";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_username", this.username));
                mySqlCommand.Parameters.Add(new MySqlParameter("_password", this.password));
                mySqlCommand.Parameters.Add(new MySqlParameter("_response", 0));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", 0));
                mySqlCommand.Parameters["_response"].Direction = ParameterDirection.Output;
                mySqlCommand.Parameters["_user_id"].Direction = ParameterDirection.Output;

                mySqlCommand.ExecuteNonQuery();

                var result = mySqlCommand.Parameters["_response"].Value;
                int userIdResult = Int32.Parse(mySqlCommand.Parameters["_user_id"].Value.ToString());

                //if result is 1, it means stored procedure ran successfully without any error 
                if (Convert.ToInt32(result) == 1)
                {
                    userId = userIdResult;
                }
            }
            catch (Exception ex)
            {
                //Log exception
                //LogHandler.LogError("UserDataHandler.createUser()", ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();           //closing DB connection
                this.password = "";     //clearing from variable memory
            }

            return userId;
        }

        public bool updateUser()
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();

            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "update_user";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_password", this.password));
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
                this.password = "";     //clearing from variable memory
            }
            return response;
        }
    }
}
