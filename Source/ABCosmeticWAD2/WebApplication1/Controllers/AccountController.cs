using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ABCostmeticWAD2.BAL;
using ABCostmeticWAD2.BAL.DTO;
using ABCostmeticWAD2.BAL.Interfaces;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMembershipService _membershipService = new MembershipSevice();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(MembershipModel model)
        {
            var user = _membershipService.Login(model.UserName, model.Password);
            if (user != null)
            {
                EmployeeDto = _membershipService.GetUserInfo(user.EmployeeId);
                var userRole = _membershipService.GetUserRole(user.EmployeeId);
                FormsAuthentication.SetAuthCookie(user.EmployeeId.ToString(), true);

                var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, userRole);
                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMsg = "Username or password is invalid!";
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult GetUserInfo()
        {
            var info = EmployeeDto;
            return Json(info, JsonRequestBehavior.AllowGet);
        }
    }
}