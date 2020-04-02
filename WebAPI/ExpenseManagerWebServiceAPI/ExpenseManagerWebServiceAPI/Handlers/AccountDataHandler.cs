using ExpenseManager.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public class AccountDataHandler : Account, IAccountDataHandler
    {
        private readonly IConfiguration config;

        public AccountDataHandler(IConfiguration config) {
            this.config = config;
        }
        public bool createAccount()
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();

            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "create_account";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
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

        public Account getAccount(int userId)
        {
            Account account = new Account();
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "get_account";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));

                //mySqlCommand.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    account.userId = userId;
                    account.email = reader["email"].ToString();
                    account.firstName = reader["first_name"].ToString();
                    account.lastName = reader["last_name"].ToString();
                    account.phone = reader["phone"].ToString();                    
                }

            }
            catch (Exception ex)
            {
                //Log exception
                System.Diagnostics.Debug.WriteLine(ex.Message);
                account = null;
                return account;
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return account;
        }

        public bool updateAccount()
        {
            throw new NotImplementedException();
        }
    }
}
