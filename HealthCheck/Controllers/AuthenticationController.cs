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
        public ActionResult Signup(UserAccountModel user = null)
        {
            Session.Clear();
            Session.Abandon();

            return View(user);
        }

        // GET: Authentication/Details/5
        public ActionResult Login(UserAccountModel user)
        {
            return View(user);
        }

        public ActionResult Logout()
        {
            
            try
            {
                UserOperations.Logout(Convert.ToString(Session["UserID"]));
            }
            catch (Exception ex)
            {
            }
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        // POST: Authentication/Create
        [HttpPost]
        public ActionResult Create(UserAccountModel user)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string result = UserOperations.SignUp(user);
                    if (int.TryParse(result, out int value))
                    {
                        Session["User"] = user.FullName;
                        Session["UserID"] = result;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        user.ModelError = result;
                        user.Password = "";
                        return RedirectToAction("Signup", user);
                    }
                }
                catch (Exception ex)
                {
                    user.ModelError = ex.Message;
                    user.Password = "";
                    return RedirectToAction("Signup", user);
                }
            }
            else
            {
                user.ModelError = "Invalid Data!";
                user.Password = "";
                return RedirectToAction("Signup", user);
            }
        }

        // POST: Authentication/Check
        [HttpPost]
        public ActionResult Check(UserAccountModel user)
        {
            try
            {
                var result = UserOperations.Login(user.EmailID, user.Password);
                if (int.TryParse(result.Item1, out int value))
                {
                    Session["User"] = result.Item2;
                    Session["UserID"] = result.Item1;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    user.ModelError = result.Item1;
                    user.Password = "";
                    return RedirectToAction("Login", user);
                }
            }
            catch (Exception ex)
            {
                user.ModelError = ex.Message;
                user.Password = "";
                return RedirectToAction("Login", user);
            }
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
