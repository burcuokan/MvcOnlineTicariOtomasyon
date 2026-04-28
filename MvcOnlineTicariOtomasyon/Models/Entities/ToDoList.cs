using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class ToDoList
    {
        [Key]
        public int ToDoListID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "bit")]
        public bool Situation { get; set; }
       
    }
}