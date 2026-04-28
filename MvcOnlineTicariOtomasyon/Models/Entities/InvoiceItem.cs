using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int InvoiceID { get; set; }
        [ForeignKey("InvoiceID")]
        public virtual Invoice Invoices { get; set; }
    }
}