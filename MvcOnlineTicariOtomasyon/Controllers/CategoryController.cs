using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        GenericRepository<Category> repo = new GenericRepository<Category>();
        
        //Listeleme ve Sayfalama İşlemi
        public ActionResult Index(int page=1)
        {
            var values = repo.List().ToPagedList(page,3);
            return View(values);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            var category = repo.TGet(id);
            repo.TDelete(category);
            return RedirectToAction("Index");
        }
        public ActionResult GetCategory(int id)
        {
            var category = repo.TGet(id);
            return View("GetCategory", category);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category p)
        {
            var category = repo.TGet(p.CategoryID);
            category.CategoryName = p.CategoryName;
            repo.TUpdate(category);
            return RedirectToAction("Index");
        }
    }
}
