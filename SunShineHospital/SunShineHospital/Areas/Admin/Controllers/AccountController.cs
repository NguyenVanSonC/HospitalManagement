using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;
using SunShineHospital.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SunShineHospital.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        #region InitController

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IPatientService _patientService;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IPatientService patientService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._patientService = patientService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    string roleUser = _userManager.GetRoles(user.Id).FirstOrDefault();
                    if (roleUser != "Doctor" && roleUser != "Admin")
                    {
                        TempData["error"] = string.Format("Xin lỗi, Bạn không đủ quyền truy nhập!");
                        return View();
                    }
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        TempData["message"] = string.Format("Xin chào {0}", user.FullName);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        TempData["message"] = string.Format("Xin chào {0}", user.FullName);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }

            return View(model);
        }
    }
}