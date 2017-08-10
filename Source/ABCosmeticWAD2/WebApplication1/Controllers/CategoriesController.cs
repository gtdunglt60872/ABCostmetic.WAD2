using System;
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

        [HttpPost]
        public ActionResult Create(CategoryModel model)
        {
            var res = new BaseResponse();
            if (model == null || string.IsNullOrEmpty(model.CategoryName) || string.IsNullOrEmpty(model.CategoryName))
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }

            try
            {
                _categoryService.Add(model);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            var res = new BaseResponse();
            if (model == null || string.IsNullOrEmpty(model.CategoryName) || string.IsNullOrEmpty(model.CategoryName))
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            try
            {
                _categoryService.Update(model);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                res.Msg = "Cannot edit this item!";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var res = new BaseResponse();
            if (id == 0)
            {
                res.Msg = Constants.MsgFail;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            try
            {
                _categoryService.Delete(id);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                res.Msg = "Cannot delete this item!";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

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