using MySql.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("username", TypeName = "varchar(255)")]
        public string username { get; set; }
        [Required(ErrorMessage = "- Enter password")]
        [Column("password", TypeName = "varchar(255)")]
        public string password { get; set; }
        [Required(ErrorMessage = "- Enter role")]
        [Column("role", TypeName = "varchar(255)")]
        public string role { get; set; }
        [Required(ErrorMessage = "- Enter email")]
        [Column("email", TypeName = "varchar(255)")]
        public string email { get; set; }
        [Required(ErrorMessage = "- Enter customer id")]
        [Column("id_khachhang", TypeName = "int")]
        public int id_khachhang { get; set; }
        [Required(ErrorMessage = "- Unknown status")]
        [Column("active", TypeName = "tinyint")]
        public int active { get; set; }
    }
}