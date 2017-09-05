using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Account.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace Account.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View(model);
            }

            byte[] salt = Encoding.ASCII.GetBytes("\n%{icN$?e?");

            string passString = "6^f?\n{x???\u0014?ovD?\bK?-?????Id?H?!G";

            byte[] enteredPassword = GenerateHash(Encoding.ASCII.GetBytes(model.Password), salt, 100, 32);
            string enteredPasswordString = ASCIIEncoding.ASCII.GetString(enteredPassword);

            if (model.Username == "travelwac" && (passString == enteredPasswordString))
            {
                FormsAuthentication.SetAuthCookie("travelwac", false);

                return RedirectToAction("index", "admin");
            } else
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View(model);
            }    
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Account");
        }

        byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }
    }
}