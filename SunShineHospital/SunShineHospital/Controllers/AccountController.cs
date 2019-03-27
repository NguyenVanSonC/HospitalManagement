using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SunShineHospital.Common;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Web.App_Start;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SunShineHospital.Service;

namespace SunShineHospital.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IPatientService _patientService;

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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại !");
                }

                var userByUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("email", "Tài khoản đã tồn tại");
                    return View(model);
                }

                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    BirthDay = model.BirthDay,
                    Gender = model.Gender,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address
                };

                await _userManager.CreateAsync(user, model.Password);
                var userRole = await _userManager.FindByEmailAsync(model.Email);
                if (userRole != null)
                {
                    await _userManager.AddToRolesAsync(userRole.Id, new string[] { "Patient" });
                }

                var patient = new Patient()
                {
                    UserId = userRole.Id,
                    Status = true,
                    Alias = GetAliasByName.GetAlias(user.FullName),
                    CreatedDate = DateTime.Now,
                    CreatedBy = userRole.UserName
                };
                _patientService.Add(patient);
                _patientService.Save();
                /*string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/template/newuser.html"));
                content = content.Replace("{{UserName}}", userRole.FullName);
                content = content.Replace("{{Link}}", ConfigHepper.GetByKey("CurrentLink") + "dang-nhap.html");

                MailHelper.SendMail(userRole.Email, "Đăng ký thành công", content);*/
                TempData["message"] = string.Format("Tài khoản {0} đã tạo thành công !", userRole.UserName);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

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

        [HttpPost]
        public JsonResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return Json(new
            {
                status = true
            });
        }
    }
}