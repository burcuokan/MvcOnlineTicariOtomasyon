using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Departman
        GenericRepository<Department> repo = new GenericRepository<Department>();
        GenericRepository<Employee> employee = new GenericRepository<Employee>();
        GenericRepository<SaleHistory> sales = new GenericRepository<SaleHistory>();
        public ActionResult Index()
        {
            var department = repo.List(x=>x.Situation==true);
            return View(department);
        }
        [Authorize(Roles ="A")]
        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartment(Department p)
        {
            p.Situation = true;
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteDepartment(int id)
        {
            var department = repo.TGet(id);
            department.Situation = false;
            repo.TUpdate(department);
            return RedirectToAction("Index");
        }
        public ActionResult GetDepartment(int id)
        {
            var department = repo.TGet(id);
            return View("GetDepartment", department);
        }
        public ActionResult UpdateDepartment(Department p)
        {
            var department = repo.TGet(p.DepartmentID);
            department.DepartmentName = p.DepartmentName;
            repo.TUpdate(department);            
            return RedirectToAction("Index");
        }
        public ActionResult DepartmentDetail(int id)
        {
            //Departman Adı Taşıma
            var name = repo.TGet(id).DepartmentName;
            ViewBag.d = name;

            var values = employee.List(x => x.DepartmentID == id);
            return View(values);
        }
        public ActionResult DepartmentPersonnelSales(int id)
        {
            //Personel Adı Taşıma
            var name = employee.TGet(id).EmployeeName + " " + employee.TGet(id).EmployeeSurname;
            ViewBag.dpers = name;

            var values = sales.List(x => x.EmployeeID == id);
            return View(values);
        }

    }
}