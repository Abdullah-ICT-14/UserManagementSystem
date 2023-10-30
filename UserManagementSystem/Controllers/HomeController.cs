using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        public ActionResult SignUp(User model)
        {

            var existingUser = db.users.FirstOrDefault(u => u.UserId == model.UserId);
            if (existingUser != null)
            {
                ModelState.AddModelError("UserId", "User ID already exists. Please Enter Unique User Id");
                ViewBag.Message = "User ID already exists";
                return View();
            }


            var user = new User
            {
                UserId = model.UserId,
                EmployeeId = model.EmployeeId,
                Name = model.Name,
                Mobile = model.Mobile,
                Email = model.Email,
               
            };

            db.users.Add(user);

            var specialCode = new SpecialCode
            {
                UserId = user.UserId,
                Code = GenerateUniqueNumber(), // Implement this method to generate a unique number
                Mobile = user.Mobile,
                Email = user.Email
            };

            db.specialCodes.Add(specialCode);

            var salt = GenerateHash(); // implement this method to hash the date and time
            var hashedPassword = DoubleHashed(salt + specialCode.Code);

            var passwordEntry = new Password
            {
                UserId = user.UserId,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            db.passwords.Add(passwordEntry);


            db.SaveChanges();

            return RedirectToAction("Index");
        }
















        //[HttpPost]
        //public ActionResult SignaUp(User user,string password)
        //{

        //    // Generate a random salt
        //    var rng = new RNGCryptoServiceProvider();
        //    byte[] salt = new byte[16];
        //    rng.GetBytes(salt);

        //    // Hash the current date and time to use as the salt
        //    string dateSalt = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

        //    // Generate a random special code
        //    int specialCode = new Random().Next();

        //    // Combine the password, dateSalt and specialCode to create the final password to be hashed
        //    string finalPassword = password + dateSalt + specialCode;

        //    // Hash the finalPassword
        //    var pbkdf2 = new Rfc2898DeriveBytes(finalPassword, salt, 10000);
        //    byte[] hash = pbkdf2.GetBytes(20);

        //    // Combine the salt and password hash for storage
        //    byte[] hashBytes = new byte[36];
        //    Array.Copy(salt, 0, hashBytes, 0, 16);
        //    Array.Copy(hash, 0, hashBytes, 16, 20);

        //    // Convert to a Base64 string for storage
        //    user.Password = Convert.ToBase64String(hashBytes);

        //    if (ModelState.IsValid)
        //    {
        //        // Save the user to the database
        //        db.users.Add(user);
        //        db.SaveChanges();

        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(user);
        //}

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

        // To generate special code
        public string GenerateUniqueNumber()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        // To generate hash code
        public string GenerateHash()
        {
            string dateTimeString = DateTime.Now.ToString();

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(dateTimeString));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // Hashed password with salt and special code
        public string DoubleHashed(string saltSpecialCode)
        {
            

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltSpecialCode));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}