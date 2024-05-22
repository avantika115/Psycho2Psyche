
using System;
using System.Configuration;

namespace HealthCheck.Models
{
    public class DBConfig
    {
        public static string connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["localhost"]);

    }
}