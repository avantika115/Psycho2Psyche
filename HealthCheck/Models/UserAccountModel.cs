using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class UserAccountModel
    {
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string password { get; set; }
        public bool IsAdult { get; set; }
    }
}