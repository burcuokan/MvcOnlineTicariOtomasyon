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
    public class EmployeeController : Controller
    {
        // GET: Employee
        GenericRepository<Employee> repo = new GenericRepository<Employee>();
        GenericRepository<Department> repodepartment = new GenericRepository<Department>();
        
        void GetEmployeeDropdown()
        {
            ViewBag.dpt = repodepartment.GetDropdownList(x => x.DepartmentName, x => x.DepartmentID.ToString());
        }
        public ActionResult Index()
        {
            var employee = repo.List();
            return View(employee);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            GetEmployeeDropdown();
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee p)
        {
            if (Request.Files.Count>0)
            {
                //Dosya adı 
                string filename = Path.GetFileName(Request.Files[0].FileName);
                //Dosya uzantısı
                string extension = Path.GetExtension(Request.Files[0].FileName);
                // Dosyanın kaydedileceği yol
                string path= "~/Image/" + filename;
                //Dosyayı sunucuya kaydetme işlemi
                Request.Files[0].SaveAs(Server.MapPath(path));
                //Veritabanındaki Image sütununa dosya yolunu atama 
                p.EmployeeImage= "/Image/" + filename;
            }
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult GetEmployee(int id)
        {
            GetEmployeeDropdown();
            var employee = repo.TGet(id);
            return View("GetEmployee",employee);
        }
        public ActionResult UpdateEmployee(Employee p)
        {
            if (Request.Files.Count > 0)
            {
                //Dosya adı 
                string filename = Path.GetFileName(Request.Files[0].FileName);
                //Dosya uzantısı
                string extension = Path.GetExtension(Request.Files[0].FileName);
                // Dosyanın kaydedileceği yol
                string path = "~/Image/" + filename;
                //Dosyayı sunucuya kaydetme işlemi
                Request.Files[0].SaveAs(Server.MapPath(path));
                //Veritabanındaki Image sütununa dosya yolunu atama 
                p.EmployeeImage = "/Image/" + filename;
            }
            repo.TUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeList()
        {
            var query = repo.List();
            return View(query);
        }
    }
}