using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.ViewModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public class TransactionDataHandler : Transaction, ITransactionDataHandler
    {
        private readonly IConfiguration config;
        public TransactionDataHandler(IConfiguration config)
        {
            this.config = config;
        }
        public bool createTransaction()
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "create_transaction";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_transaction_id", this.transactionId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_title", this.title));
                mySqlCommand.Parameters.Add(new MySqlParameter("_type", this.type));
                mySqlCommand.Parameters.Add(new MySqlParameter("_amount", this.amount));
                mySqlCommand.Parameters.Add(new MySqlParameter("_friend_id", this.friendId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_picture", this.transactionPicture));
                mySqlCommand.Parameters.Add(new MySqlParameter("_date_created", this.dateCreated));
                mySqlCommand.Parameters.Add(new MySqlParameter("_date_updated", this.dateUpdated));
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

        public List<ActivityViewModel> getAllActivity(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<ActivityViewModel> activityViewModels = new List<ActivityViewModel>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "get_all_activities";  //CREATE STORE PROCEDURE
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));

                //mySqlCommand.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    ActivityViewModel activityViewModel = new ActivityViewModel();
                    activityViewModel.amount = Double.Parse(reader["amount"].ToString());
                    activityViewModel.firstName = reader["first_name"].ToString();
                    activityViewModel.lastName = reader["last_name"].ToString();
                    activityViewModel.transactionTitle = reader["title"].ToString();
                    activityViewModels.Add(activityViewModel); //adding to Vendor List
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
            return activityViewModels;
        }
    }
}
