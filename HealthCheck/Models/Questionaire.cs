using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class Questionaire
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public List<QuestionData> QuestionData { get; set;}
    }

    public class QuestionData
    {
        public int questionno { get; set; }
        public int scale { get; set; }
        public string option { get; set; }
    }
}