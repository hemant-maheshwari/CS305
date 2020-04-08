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
    public class TotalDataHandler: Total, ITotalDataHandler
    {
        private readonly IConfiguration config;
        public TotalDataHandler(IConfiguration config)
        {
            this.config = config;
        }

        public bool createTotal()
        {
            return saveTotal("create_total");
        }

        public Total getTotal(int userId)
        {
            Total total = new Total();
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;

                mySqlCommand.CommandText = "get_total";
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));

                //mySqlCommand.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    total.userId = userId;
                    total.incomeAmount = Double.Parse(reader["income_amount"].ToString());
                    total.expenseAmount = Double.Parse(reader["expense_amount"].ToString());
                }

            }
            catch (Exception ex)
            {
                //Log exception
                System.Diagnostics.Debug.WriteLine(ex.Message);
                total = null;
                return total;
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return total;
        }

        public bool updateTotal()
        {
            return saveTotal("update_total");
        }

        private bool saveTotal(string command) {
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
                mySqlCommand.Parameters.Add(new MySqlParameter("_income_amount", this.incomeAmount));
                mySqlCommand.Parameters.Add(new MySqlParameter("_expense_amount", this.expenseAmount));
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
