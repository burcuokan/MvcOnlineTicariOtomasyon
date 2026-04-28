using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CargoController : Controller
    {
        // GET: Cargo
        GenericRepository<CargoDetail> repo = new GenericRepository<CargoDetail>();
        GenericRepository<CargoTracking> repocargo = new GenericRepository<CargoTracking>();
        public ActionResult Index(string p)
        {
            var cargo = repo.List(x => string.IsNullOrEmpty(p) || x.TrackingCode.Contains(p));
            return View(cargo.ToList());
        }        
        [HttpGet]
        public ActionResult NewCargo()
        {
            Random rnd = new Random();
            string[] characters = { "A", "B", "C", "D" };
            int k1, k2, k3;
            k1 = rnd.Next(0, 4);
            k2 = rnd.Next(0, 4);
            k3 = rnd.Next(0, 4);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string code = s1.ToString() + characters[k1] + s2 + characters[k2] + s3 + characters[k3];
            ViewBag.trackingcode = code;
            return View();
        }
        [HttpPost]
        public ActionResult NewCargo(CargoDetail c)
        {
            repo.TAdd(c);
            return RedirectToAction("Index");
        }
        public ActionResult CargoTracking(string id)
        {
            var values = repocargo.List(x => x.TrackingCode == id); 
            return View(values);
        }
    }
}