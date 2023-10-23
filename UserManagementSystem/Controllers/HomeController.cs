using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Verification()
        {
            return View();
        }

        public ActionResult SendingPassword()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}