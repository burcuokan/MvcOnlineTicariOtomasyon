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
    [AllowAnonymous]
    public class LoginController : Controller
    {
        GenericRepository<Customer> repo = new GenericRepository<Customer>();
        GenericRepository<Admin> admin = new GenericRepository<Admin>();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Partial1(Customer p)
        {
            repo.TAdd(p);
            return PartialView();
        }
        [HttpGet]
        public ActionResult CustomerLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerLogin(Customer p)
        {
            var user = repo.List(x => x.CustomerMail == p.CustomerMail && x.CustomerPassword == p.CustomerPassword).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.CustomerMail, false);
                Session["CustomerMail"] = user.CustomerMail.ToString();
                return RedirectToAction("Index", "CustomerPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var user = admin.List(x => x.Username == p.Username && x.Password == p.Password).FirstOrDefault();
            if (user!=null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                Session["UserName"] = user.Username.ToString();
                return RedirectToAction("Index","Category");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}