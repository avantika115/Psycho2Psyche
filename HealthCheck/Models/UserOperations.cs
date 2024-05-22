using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class UserOperations
    {
        public static (string, string) Login(string emailid, string password)
        {
            // Example call to user_login stored procedure
            using (MySqlConnection connection = new MySqlConnection(DBConfig.connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("user_login", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_email", emailid);
                    cmd.Parameters.AddWithValue("@p_password", SHAEncryption.Encrypt(password));

                    cmd.Parameters.Add(new MySqlParameter("@p_result", MySqlDbType.Int32));
                    cmd.Parameters["@p_result"].Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters.Add(new MySqlParameter("@full_name", MySqlDbType.VarChar,50));
                    cmd.Parameters["@full_name"].Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int result = Convert.ToInt32(cmd.Parameters["@p_result"].Value);

                    switch (result)
                    {
                        case -1:
                            return ("User not found with the provided email.","");
                            break;
                        case -2:
                            return ("User found but not active.","");
                            break;
                        case -3:
                            return  ("Incorrect password.","");
                            break;
                        default:
                            return  (result.ToString(), Convert.ToString(cmd.Parameters["@full_name"].Value));
                            break;
                    }
                }
            }
        }

        public static string SignUp(UserAccountModel user)
        {
            // Example call to insert_user stored procedure
            using (MySqlConnection connection = new MySqlConnection(DBConfig.connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("insert_user", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@p_password", SHAEncryption.Encrypt(user.Password));
                    cmd.Parameters.AddWithValue("@p_active", true);
                    cmd.Parameters.AddWithValue("@p_EmailID", user.EmailID);
                    cmd.Parameters.AddWithValue("@p_DOB", user.DOB);
                    cmd.Parameters.AddWithValue("@p_MobileNo", user.MobileNo);

                    cmd.Parameters.Add(new MySqlParameter("@p_result", MySqlDbType.Int32));
                    cmd.Parameters["@p_result"].Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int result = Convert.ToInt32(cmd.Parameters["@p_result"].Value);

                    if (result == -1)
                    {
                        return "Email ID already exists.";
                    }
                    else
                    {
                        return result.ToString();
                    }
                }
            }
        }

        public static void Logout(string userid)
        {
            // Example call to insert_user stored procedure
            using (MySqlConnection connection = new MySqlConnection(DBConfig.connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("user_logout", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@v_userId", int.Parse(userid));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}