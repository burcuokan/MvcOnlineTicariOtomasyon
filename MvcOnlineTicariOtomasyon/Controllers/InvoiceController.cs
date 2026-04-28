using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        GenericRepository<Invoice> repo = new GenericRepository<Invoice>();
        GenericRepository<InvoiceItem> repo1 = new GenericRepository<InvoiceItem>();
        public ActionResult Index()
        {
            var invoice = repo.List();
            return View(invoice);
        }
        [HttpGet]
        public ActionResult AddInvoice()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddInvoice(Invoice p)
        {
            repo.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult GetInvoice(int id)
        {
            var invoice = repo.TGet(id);
            return View("GetInvoice", invoice);
        }
        public ActionResult UpdateInvoice(Invoice p)
        {
            repo.TUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult InvoiceDetail(int id)
        {
            var values = repo1.List(x => x.InvoiceID == id);
            return View(values);

        }
        [HttpGet]
        public ActionResult NewPen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPen(InvoiceItem p)
        {
            repo1.TAdd(p);
            return RedirectToAction("Index");
        }
        public ActionResult Dynamic()
        {
            InvoiceDynamicModel cs = new InvoiceDynamicModel();
            cs.value1 = repo.List();
            cs.value2 = repo1.List();
            return View(cs);
        }
        public ActionResult SaveInvoice(Invoice p, InvoiceItem[] invoiceitem)
        {
            repo.TAdd(p);
            //Kalemler bir Dizi(Array) olduğu için içinde foreach dönmemiz lazım.
            foreach (var x in invoiceitem)
            {
                x.InvoiceID = p.InvoiceID;
                repo1.TAdd(x);
            }
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}

//Invoices p: Faturanın "kapak" bilgisidir, tektir.
//InvoiceItems[]: Faturanın "içindeki satırlardır, çoktur.