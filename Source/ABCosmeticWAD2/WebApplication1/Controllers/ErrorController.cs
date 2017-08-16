using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error401()
        {
            const string msg = "You dont have permission! Please contact Admin!";
            ViewBag.Msg = msg;
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult Error400()
        {
            return View();
        }
    }
}