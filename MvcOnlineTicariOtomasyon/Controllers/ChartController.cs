using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        GenericRepository<Product> repo = new GenericRepository<Product>();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var graphics = new Chart(600, 600);
            graphics.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Beyaz Eşya", "Küçük Ev Aletleri", "Bilgisayar" }, yValues: new[] { 85, 66, 98 }).Write();
            return File(graphics.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList(); //Ürün İsimleri
            ArrayList yvalue = new ArrayList(); //Stok Miktarları
            var results = repo.List();
            results.ToList().ForEach(x => xvalue.Add(x.ProductName));
            results.ToList().ForEach(y => yvalue.Add(y.Stock));
            var chart = new Chart(width: 500, height: 500).AddTitle("Stoklar").AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(chart.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult VisualizeProductResult()
        {
            return Json(Productlist(), JsonRequestBehavior.AllowGet);
        }
        public List<ChartClass> Productlist()
        {
            List<ChartClass> snf = new List<ChartClass>();
            snf.Add(new ChartClass()
            {
                ProductName = "Bilgisayar",
                Stock = 120
            });
            snf.Add(new ChartClass()
            {
                ProductName = "Beyaz Eşya",
                Stock = 150
            });
            snf.Add(new ChartClass()
            {
                ProductName = "Mobilya",
                Stock = 70
            });
            snf.Add(new ChartClass()
            {
                ProductName = "Küçük Ev Aletleri",
                Stock = 180
            });
            snf.Add(new ChartClass()
            {
                ProductName = "Mobil Cihazlar",
                Stock = 90
            });
            return snf;
        }
        public ActionResult Index5()
        {
            return View();
        }
        public ActionResult VisualizeProductResult2()
        {
            return Json(Productlist2(), JsonRequestBehavior.AllowGet);
        }
        public List<ChartProductClass> Productlist2()
        {
            List<ChartProductClass> product = new List<ChartProductClass>();
            product = repo.List().Select(x => new ChartProductClass
            {
                prdc = x.ProductName,
                stc = x.Stock
            }).ToList();
            return product;
        }
        public ActionResult Index6()
        {
            return View();
        }
        public ActionResult Index7()
        {
            return View();
        }
    }
}
