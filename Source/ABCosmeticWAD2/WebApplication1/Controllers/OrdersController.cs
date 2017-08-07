using System.Web.Mvc;
using ABCostmeticWAD2.BAL;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListData()
        {
            var categoryService = new CategoryService();
            var list = categoryService.GetAll();

            return Json(new
            {
                Data = list
            }, JsonRequestBehavior.AllowGet);
        }
    }
}