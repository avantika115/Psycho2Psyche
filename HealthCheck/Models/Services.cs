using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class ServicesModel
    {
        public string Name { get; set; }
        public List<Questionaire> Questions { get; set; }
        public int TotalScore { get; set; }
        public string contenttitle { get; set; }
        public string contentdescription { get; set; }
        public string Report { get; set; }

        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
        public int PatientID { get; set; }
    }

    public class Services
    {
        public ServicesModel Service { get; set; }
        public Services(string ServiceCalled)
        {
            Service = new ServicesModel();
            Service.Name = ServiceCalled;
   
        }

        public Services(string ServiceCalled, string testpage)
        {
            Service = new ServicesModel();
            Service.Name = ServiceCalled;
            Service.Questions = getQuestions(ServiceCalled);
        }

        List<Questionaire> getQuestions(string servicecalled)
        {

            List < Questionaire > Questionaire  = new List<Questionaire>();
            using (MySqlConnection connection = new MySqlConnection(DBConfig.connectionString))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter("getQuestionnaire", connection))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@service_name", servicecalled);
                    DataSet Ds = new DataSet();
                    da.Fill(Ds);

                    Questionaire = (
                    from DataRow dr in Ds.Tables[0].Rows
                    select new Questionaire()
                    {
                        QuestionID = int.Parse(dr["questionno"].ToString()),
                        Question = Convert.ToString(dr["question"]),
                    }).ToList<Questionaire>();

                    foreach (var question in Questionaire)
                    {
                        question.QuestionData = (
                        from DataRow dr in Ds.Tables[1].Rows
                        where int.Parse(dr["questionno"].ToString()) == question.QuestionID 
                        select new QuestionData()
                        {
                            questionno = int.Parse(dr["questionno"].ToString()),
                            scale = int.Parse(dr["scale"].ToString()),
                            option = Convert.ToString(dr["description"])
                        }).ToList<QuestionData>();
                    }
                }
            }

            return Questionaire;
        }


        public static string GenerateReport(int TotalScore, string ServiceName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DBConfig.connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Create command object
                        MySqlCommand command = new MySqlCommand("checkReport", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Add input parameters
                        command.Parameters.AddWithValue("@service_name", ServiceName);
                        command.Parameters.AddWithValue("@total_score", TotalScore);

                        // Add output parameter
                        command.Parameters.Add("@report_description", MySqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;

                        // Execute the command
                        command.ExecuteNonQuery();

                        // Get the output parameter value
                        string reportDescription = command.Parameters["@report_description"].Value.ToString();

                        // Output the report description
                        return reportDescription;
                    }
                    catch (Exception ex)
                    {
                        return "Error: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }
           
        }
    }
}