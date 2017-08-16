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
    [Authorize]
    public class BaseController : Controller
    {
        public EmployeeDto EmployeeDto { get; set; }
    }
}