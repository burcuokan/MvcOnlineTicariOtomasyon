using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        Context c = new Context();
        public ActionResult Index()
        {
            //Toplam Müşteri 
            var totalCustomer = c.Customers.Where(x => x.Situation == true).Count().ToString();
            ViewBag.cst = totalCustomer;
            //Toplam Ürün 
            var totalProduct = c.Products.Where(x => x.Situation == true).Count().ToString();
            ViewBag.prd = totalProduct;
            //Toplam Personel
            var totalEmployee = c.Employees.Count().ToString();
            ViewBag.emp = totalEmployee;
            //Toplam Kategori
            var totalCategory = c.Categories.Count().ToString();
            ViewBag.ctg = totalCategory;
            //Toplam Stok
            var totalStock = c.Products.Where(x=>x.Situation==true).Sum(x => x.Stock).ToString();
            ViewBag.stc = totalStock;
            //Marka Sayısı
            var totalBrand = c.Products.Where(x => x.Situation == true).Select(x => x.Brand).Distinct().Count();
            ViewBag.brd = totalBrand;
            //Kritik Seviye
            var criticalLevel = c.Products.Where(x => x.Situation == true).Count(x => x.Stock <= 20).ToString();
            ViewBag.crt = criticalLevel;
            //Max Fiyatlı Ürün
            var maxPrice = c.Products.Where(x => x.Situation == true).OrderByDescending(x => x.SalePrice).Select(y => y.ProductName).FirstOrDefault();
            ViewBag.prc = maxPrice;
            //Min Fiyatlı Ürün
            var minPrice = c.Products.Where(x => x.Situation == true).OrderBy(x => x.SalePrice).Select(y => y.ProductName).FirstOrDefault();
            ViewBag.prc2 = minPrice;
            //Max Marka
            var brand = c.Products.Where(x => x.Situation == true).GroupBy(x => x.Brand).OrderByDescending(y => y.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.brd1 = brand;
            //Buzdolabı Sayısı
            var totalFridge = c.Products.Where(x => x.Situation == true).Count(x => x.ProductName == "Buzdolabı").ToString();
            ViewBag.frd = totalFridge;
            //Laptop Sayısı
            var totalLaptop = c.Products.Where(x => x.Situation == true).Count(x => x.ProductName == "Laptop").ToString();
            ViewBag.lpt = totalLaptop;
            //En Çok Satılan Ürün
            var sales = c.Products.Where(u => u.ProductID == (c.SaleHistories.GroupBy(x => x.ProductID).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.ProductName).FirstOrDefault();
            ViewBag.sl = sales;
            //Kasadaki Tutar
            var totalPrice = c.SaleHistories.Sum(x => x.TotalPrice).ToString();
            ViewBag.tpc = totalPrice;
            //Bugünkü Satışlar
            DateTime today = DateTime.Today;
            var sales1 = c.SaleHistories.Count(x => x.Date == today).ToString();
            ViewBag.sls = sales1;
            //Bugünkü Kasa
            var safe = c.SaleHistories.Where(x => x.Date == today).Sum(y =>(decimal?) y.TotalPrice).ToString();
            ViewBag.sf = safe;
            return View();
        }
        public ActionResult SimpleTables()
        {
            var query = from x in c.Customers
                        group x by x.CustomerCity into g // Müşterileri Şehirlerine göre grupla, sonucu 'g' içine at.
                        select new ClassGroup
                        {
                            City = g.Key,
                            Number = g.Count()
                        };
            return View(query.ToList());
        }
        public PartialViewResult Partial1()
        {
            var query2 = from x in c.Employees
                        group x by x.Department.DepartmentName into g
                        select new ClassGroup2
                        {
                            Department = g.Key,
                            Number = g.Count()
                        };
            return PartialView(query2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var query = c.Customers.ToList();
            return PartialView(query);
        }
        public PartialViewResult Partial3()
        {
            var query = c.Products.ToList();
            return PartialView(query);
        }
        public PartialViewResult Partial4()
        {
            var query = from x in c.Products
                        group x by x.Brand into g
                        select new ClassGroup3
                        {
                            Brand = g.Key,
                            Number = g.Count()
                        };
            return PartialView(query.ToList());
        }
    }
}