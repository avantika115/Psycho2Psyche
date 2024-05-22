using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class Questionaire
    {
        public int QuestionID { get; set; }
        public List<string> Questions { get; set; }
        public List<int> Answer { get; set; }
    }
}