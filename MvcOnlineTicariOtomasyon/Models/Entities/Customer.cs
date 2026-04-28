using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30,ErrorMessage ="En fazla 30 karakter yazabilirsiniz.")]
        public string CustomerName { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu alanı boş geçemezsiniz!")]
        public string CustomerSurname { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string CustomerCity { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CustomerMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string CustomerPassword { get; set; }
        public bool Situation { get; set; }
        public ICollection<SaleHistory> SaleHistories { get; set; }
    }
}