using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_khachhang", TypeName = "int")]
        public int id_khachhang { get; set; }
        [Required(ErrorMessage = "- Enter name")]
        [Column("ten_kh", TypeName = "varchar(255)")]
        public string ten_kh { get; set; }
        [Column("diachi", TypeName = "varchar(255)")]
        public string diachi { get; set; }
        [Required(ErrorMessage = "- Enter phone number")]
        [Column("sodt", TypeName = "varchar(255)")]
        public string sodt { get; set; }
        [Required(ErrorMessage = "- Enter email")]
        [Column("email", TypeName = "varchar(255)")]
        public string email { get; set; }
    }
}