using MVCModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModule.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            var owinctx = Request.GetOwinContext();
            owinctx.Authentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var owinctx = Request.GetOwinContext();
                using (var userManager = ServiceLocator.Resolve<IUserManager>())
                {
                    var user = userManager.Find(model.UserName, model.Password);
                    if (user != null)
                    {
                        var identity = userManager.CreateIdentity(user);
                        owinctx.Authentication.SignIn(identity);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The username or password provided is incorrect.");
            return View(model);
        }



    }
}