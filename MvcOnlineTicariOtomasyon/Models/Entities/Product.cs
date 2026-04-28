using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string ProductName { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Brand { get; set; }
        public short Stock { get; set; }
        public decimal PurshasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool Situation { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string ProductImage { get; set; }
        public int Kategoriid { get; set; }
        [ForeignKey("Kategoriid")]
        public virtual Category Category { get; set; }
        public ICollection<SaleHistory> SaleHistories { get; set; }
    }
}