using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        GenericRepository<Customer> repo = new GenericRepository<Customer>();
        GenericRepository<SaleHistory> repo1 = new GenericRepository<SaleHistory>();
        public ActionResult Index()
        {
            var customers = repo.List(x=>x.Situation==true);
            return View(customers);
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer p)
        {
            p.Situation = true;
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCustomer(int id)
        {
            var customer = repo.TGet(id);
            customer.Situation = false;
            repo.TUpdate(customer);
            return RedirectToAction("Index");         
        }
        public ActionResult GetCustomer(int id)
        {
            var customer = repo.TGet(id);
            return View("GetCustomer", customer);
        }
        public ActionResult UpdateCustomer(Customer p)
        {
            if (!ModelState.IsValid)
            {
                return View("GetCustomer");
            }
            repo.TUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult CustomerSales(int id)
        {
            //Müşteri Adı Taşıma
            var customer = repo.TGet(id).CustomerName + " " + repo.TGet(id).CustomerSurname;
            ViewBag.cst = customer;

            var values = repo1.List(x => x.CustomerID == id);
            return View(values);
        }
    }
}