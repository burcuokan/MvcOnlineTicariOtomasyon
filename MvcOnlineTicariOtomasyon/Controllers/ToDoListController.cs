using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ToDoListController : Controller
    {
        // GET: ToDoList
        Context c = new Context();
        GenericRepository<ToDoList> repo = new GenericRepository<ToDoList>();
        public ActionResult Index()
        {
            var CustomerCount = c.Customers.Where(x=>x.Situation==true).Count().ToString();
            ViewBag.cst = CustomerCount;
            var ProductCount = c.Products.Where(x=>x.Situation==true).Count().ToString();
            ViewBag.prd = ProductCount;
            var CategoryCount = c.Categories.Count().ToString();
            ViewBag.ctg = CategoryCount;
            var CityCount = c.Customers.Where(x=>x.Situation==true).Select(x => x.CustomerCity).Distinct().Count();
            ViewBag.cty = CityCount;

            var toDoList = c.ToDoLists.ToList();
            return View(toDoList);
        }
        [HttpGet]
        public ActionResult AddToDo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddToDo(ToDoList t)
        {
            t.Situation = false;
            repo.TAdd(t);
            return RedirectToAction("Index");
        }
    }
}
