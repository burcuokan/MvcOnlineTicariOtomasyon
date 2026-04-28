using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        GenericRepository<Product> repo = new GenericRepository<Product>();
        GenericRepository<Category> repoCategory = new GenericRepository<Category>();
        GenericRepository<Employee> repoEmployee = new GenericRepository<Employee>();
        GenericRepository<SaleHistory> reposale = new GenericRepository<SaleHistory>();
        void GetCategoryDropdown()
        {
            ViewBag.vlc = repoCategory.GetDropdownList(x => x.CategoryName, x => x.CategoryID.ToString());
        }
        //Listeleme ve Arama İşlemi
        public ActionResult Index(string p)
        {
            var products = repo.List(x=>x.Situation==true && (string.IsNullOrEmpty(p) || x.ProductName.Contains(p)));
            return View(products.ToList());
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            GetCategoryDropdown();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            p.Situation = true;
            repo.TAdd(p);        
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduct(int id)
        {
            var product = repo.TGet(id);
            product.Situation = false;
            repo.TUpdate(product);
            return RedirectToAction("Index");
        }
        public ActionResult GetProduct(int id)
        {
            GetCategoryDropdown();
            var product = repo.TGet(id);
            return View("GetProduct", product);
        }
        public ActionResult UpdateProduct(Product p)
        {
            repo.TUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult ProductList()
        {
            var values = repo.List();
            return View(values);
        }
        [HttpGet]
        public ActionResult Makesale(int id)
        {
            //Personeller
            ViewBag.emp = repoEmployee.GetDropdownList(x => x.EmployeeName + " " + x.EmployeeSurname, x => x.EmployeeID.ToString());
            var values = repo.TGet(id);
            ViewBag.vls = values.ProductID;
            ViewBag.vls2 = values.SalePrice;
            return View();
        }
        [HttpPost]
        public ActionResult MakeSale(SaleHistory p)
        {
            p.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            reposale.TAdd(p);
            return RedirectToAction("Index","Sales");
        }
    }
}

