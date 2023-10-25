using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using UserManagementSystem.Models;

namespace UserManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private UserContext db = new UserContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user,string password)
        {

            // Generate a random salt
            var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            // Hash the current date and time to use as the salt
            string dateSalt = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

            // Generate a random special code
            int specialCode = new Random().Next();

            // Combine the password, dateSalt and specialCode to create the final password to be hashed
            string finalPassword = password + dateSalt + specialCode;

            // Hash the finalPassword
            var pbkdf2 = new Rfc2898DeriveBytes(finalPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and password hash for storage
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert to a Base64 string for storage
            user.Password = Convert.ToBase64String(hashBytes);

            if (ModelState.IsValid)
            {
                // Save the user to the database
                db.users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(user);
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