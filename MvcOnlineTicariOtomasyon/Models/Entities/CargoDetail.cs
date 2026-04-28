using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class CargoDetail
    {
        [Key]
        public int CargoDetailID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Explanation { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string TrackingCode { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Employee { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string Buyer { get; set; }
        public DateTime Date { get; set; }
    }
}