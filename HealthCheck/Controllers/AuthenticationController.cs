using HealthCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthCheck.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Signup()
        {
            Session.Clear();
            Session.Abandon();
            
            return View();
        }

        // GET: Authentication/Details/5
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(UserAccountModel user)
        {
            Session["User"] = user.FullName;
            return RedirectToAction("Index", "Home");
        }

        // POST: Authentication/Check
        [HttpPost]
        public ActionResult Check(UserAccountModel user)
        {
            Session["User"] = user.EmailID;
            return RedirectToAction("Index", "Home");
        }

        // GET: Authentication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Authentication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authentication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Authentication/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
