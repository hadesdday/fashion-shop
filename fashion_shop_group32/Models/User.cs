using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    public class User
    {
        [Key]
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
        [Column("token", TypeName = "varchar(255)")]
        public string token { get; set; }
    }
}