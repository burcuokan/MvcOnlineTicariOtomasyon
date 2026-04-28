using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class ClassList
    {
        public IEnumerable<SelectListItem> Categories  { get; set; }
        public IEnumerable<SelectListItem> Products  { get; set; }
    }
}