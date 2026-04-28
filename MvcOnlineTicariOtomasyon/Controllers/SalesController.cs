using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        GenericRepository<SaleHistory> repo = new GenericRepository<SaleHistory>();
        GenericRepository<Product> repoProduct = new GenericRepository<Product>();
        GenericRepository<Customer> repoCustomer = new GenericRepository<Customer>();
        GenericRepository<Employee> repoEmployee = new GenericRepository<Employee>();

        void GetDropdownData()
        {
            //Ürünler
            ViewBag.prd = repoProduct.GetDropdownList(x => x.ProductName + " - " + x.Brand, x => x.ProductID.ToString());
            //Müşteriler
            ViewBag.cst = repoCustomer.GetDropdownList(x => x.CustomerName + " " + x.CustomerSurname, x => x.CustomerID.ToString());
            //Personeller
            ViewBag.emp = repoEmployee.GetDropdownList(x => x.EmployeeName + " " + x.EmployeeSurname, x => x.EmployeeID.ToString());
        }
        public ActionResult Index()
        {
            var sales = repo.List();
            return View(sales);
        }
        [HttpGet]
        public ActionResult AddSales()
        {
            GetDropdownData();
            return View();
        }
        [HttpPost]
        public ActionResult AddSales(SaleHistory p)
        {
            p.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult GetSales(int id)
        {
            GetDropdownData();
            var sales = repo.TGet(id);
            return View("GetSales", sales);
        }
        public ActionResult UpdateSales(SaleHistory p)
        {
            repo.TUpdate(p);
            return RedirectToAction("Index");

        }
        public ActionResult SalesDetail(int id)
        {
            var values = repo.List(x => x.SaleID == id);
            return View(values);
        }
    }
}