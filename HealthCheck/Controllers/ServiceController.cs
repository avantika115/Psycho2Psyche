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
        public ActionResult SubmitTest(ServicesModel details)
        {
            details.TotalScore = 25;
            details.Report = "Severe depression";

            return View("TestResult", details);
        }
    }
}