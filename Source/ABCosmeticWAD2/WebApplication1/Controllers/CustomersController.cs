using System.Web.Mvc;
using ABCostmeticWAD2.BAL;
using ABCostmeticWAD2.BAL.Interfaces;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService = new CustomerService();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListData()
        {
            var list = _customerService.GetAll();

            return Json(new
            {
                Data = list
            }, JsonRequestBehavior.AllowGet);
        }
    }
}