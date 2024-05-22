using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCheck.Models;

namespace HealthCheck.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index(string ServiceCalled)
        {
            Session["ServiceName"] = ServiceCalled;

            Services service = new Services(ServiceCalled);

            return View(service.Service);
        }

        // GET: selft test page
        public ActionResult Selftest()
        {
            Services service = new Services(Convert.ToString(Session["ServiceName"]), "test");

            return View(service.Service);
        }

        // POST: selft test page
        public ActionResult SubmitTest(FormCollection formdata)
        {
            //details.TotalScore = 25;
            //details.Report = "Severe depression";
            ServicesModel data = new ServicesModel();
            data.PatientName = Convert.ToString(Session["User"]);
            data.PatientID = Convert.ToInt32(Session["UserID"]);
            data.Name = Convert.ToString(Session["ServiceName"]);
            
            //Score calculations
            int score=0;
            foreach (string key in formdata.Keys)
            {
                if (key.StartsWith("radio_")) // Assuming the radio button names start with "radio_"
                {
                    //string radioButtonValue = formdata[key];


                    score += Convert.ToInt32(formdata[key]);
                }
            }


            data.TotalScore = score;
            data.Report = Services.GenerateReport(data.TotalScore, data.Name);

            //Save Report
            Report.InsertIntoDatabase(data);

            return View("TestResult", data);
        }


    }
}