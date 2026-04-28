using MvcOnlineTicariOtomasyon.Models.Entities;
using MvcOnlineTicariOtomasyon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        GenericRepository<Product> repo = new GenericRepository<Product>();
        public ActionResult Index()
        {
            var values = repo.List();
            return View(values);
        }
    }
}