using System.Web.Mvc;
using ABCostmeticWAD2.BAL;
using ABCostmeticWAD2.BAL.DTO;
using ABCostmeticWAD2.BAL.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService = new CategoryService();

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Create(CategoryModel model)
        //{

        //    return Json(new{},JsonRequestBehavior.AllowGet)
        //}

        [HttpPost]
        public ActionResult ListData()
        {
            var list = _categoryService.GetAll();
            var res = new Response<CategoryModel>
            {
                ListData = list
            };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}