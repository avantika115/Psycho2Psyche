using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class Report
    {
        public static void InsertIntoDatabase(ServicesModel data)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DBConfig.connectionString))
                {
                    MySqlCommand command = new MySqlCommand("InsertReport", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PatientID", data.PatientID);
                    command.Parameters.AddWithValue("@p_TotalScore", data.TotalScore);
                    command.Parameters.AddWithValue("@p_Report", data.Report);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //
            }
           
        }

        private DataSet getReports(string fromdate, string todate)
        {
            DataSet Ds = new DataSet();
            using (MySqlConnection connection = new MySqlConnection(DBConfig.connectionString))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter("getReports", connection))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate);
                    da.SelectCommand.Parameters.AddWithValue("@todate", todate);
                    
                    da.Fill(Ds);
                }
            }

            return Ds;
        }
    }
}