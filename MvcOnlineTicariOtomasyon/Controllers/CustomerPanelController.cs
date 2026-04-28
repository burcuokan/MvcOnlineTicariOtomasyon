using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CustomerPanelController : Controller
    {
        // GET: CustomerPanel
        GenericRepository<Customer> repo = new GenericRepository<Customer>();
        GenericRepository<SaleHistory> reposales = new GenericRepository<SaleHistory>();
        GenericRepository<Messages> repomessages = new GenericRepository<Messages>();
        GenericRepository<CargoDetail> repocargo = new GenericRepository<CargoDetail>();
        GenericRepository<CargoTracking> repocargotracking = new GenericRepository<CargoTracking>();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CustomerMail"];
            var values = repomessages.List(x => x.Buyer == mail);
            ViewBag.m = mail;
            var mailid = repo.List(x => x.CustomerMail == mail).Select(y => y.CustomerID).FirstOrDefault();
            ViewBag.mid = mailid;
            var totalsales = reposales.List(x => x.CustomerID == mailid).Count();
            ViewBag.totalsales = totalsales;
            var totalamount = reposales.List(x => x.CustomerID == mailid).Sum(y => y.TotalPrice);
            ViewBag.totalamount = totalamount;
            var totalproduct = reposales.List(x => x.CustomerID == mailid).Sum(y => y.Quantity);
            ViewBag.totalproduct = totalproduct;
            var name = repo.List(x => x.CustomerMail == mail).Select(y => y.CustomerName + " " + y.CustomerSurname).FirstOrDefault();
            ViewBag.name = name;
            return View(values);
        }
        public ActionResult MyOrders()
        {
            var mail = (string)Session["CustomerMail"];
            var id = repo.List(x => x.CustomerMail == mail).Select(y => y.CustomerID).FirstOrDefault();
            var orders = reposales.List(x => x.CustomerID == id);
            return View(orders);
        }
        public ActionResult IncomingMessages()
        {
            var mail = (string)Session["CustomerMail"];
            var messages = repomessages.List(x=>x.Buyer==mail).OrderByDescending(x=>x.MessageID).ToList();
            var incomingnumber = repomessages.List(x => x.Buyer == mail).Count().ToString();
            ViewBag.d1 = incomingnumber;
            var outgoingnumber = repomessages.List(x => x.Sender == mail).Count().ToString();
            ViewBag.d2 = outgoingnumber;
            return View(messages);
        }
        public ActionResult OutgoingMessages()
        {
            var mail = (string)Session["CustomerMail"];
            var messages = repomessages.List(x => x.Sender == mail).OrderByDescending(x=>x.MessageID).ToList();
            var incomingnumber = repomessages.List(x => x.Buyer == mail).Count().ToString();
            ViewBag.d1 = incomingnumber;
            var outgoingnumber = repomessages.List(x => x.Sender == mail).Count().ToString();
            ViewBag.d2 = outgoingnumber;
            return View(messages);
        }
        public ActionResult MessageDetail(int id)
        {
            var values = repomessages.List(x => x.MessageID == id);
            var mail = (string)Session["CustomerMail"];
            var incomingnumber = repomessages.List(x => x.Buyer == mail).Count().ToString();
            ViewBag.d1 = incomingnumber;
            var outgoingnumber = repomessages.List(x => x.Sender == mail).Count().ToString();
            ViewBag.d2 = outgoingnumber;
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            var mail = (string)Session["CustomerMail"];
            var incomingnumber = repomessages.List(x => x.Buyer == mail).Count().ToString();
            ViewBag.d1 = incomingnumber;
            var outgoingnumber = repomessages.List(x => x.Sender == mail).Count().ToString();
            ViewBag.d2 = outgoingnumber;
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Messages m)
        {
            var mail = (string)Session["CustomerMail"];
            m.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Sender = mail;
            repomessages.TAdd(m);
            return View();
        }
        public ActionResult CargoTracking(string p)
        {
            var cargo = repocargo.List(x => x.TrackingCode.Contains(p));
            return View(cargo.ToList());
        }
        public ActionResult CustomerCargoTracking(string id)
        {
            var values = repocargotracking.List(x => x.TrackingCode == id);
            return View(values);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CustomerMail"];
            var id = repo.List(x => x.CustomerMail == mail).Select(y => y.CustomerID).FirstOrDefault();
            var findcustomers = repo.TGet(id);
            return PartialView("Partial1",findcustomers);
        }
        public PartialViewResult Partial2()
        {
            var data = repomessages.List(x => x.Sender == "admin");
            return PartialView(data);
        }
        public ActionResult UpdateCustomerInformation(Customer c)
        {
            repo.TUpdate(c);
            return RedirectToAction("Index");
        }
    }
}