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
    public class LoginController : Controller
    {
        private IMembershipService _membershipService= new MembershipSevice();

       
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(MembershipModel model)
        {
            var user = _membershipService.Login(model.UserName, model.Password);
            if (user!=null)
            {
                FormsAuthentication.SetAuthCookie(user.EmployeeId.ToString(), true);
                return Redirect("/");
            }

            ViewBag.ErrorMsg = "Username or password is invalid!";
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("/");
        }
    }
}