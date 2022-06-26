using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    [Table("review")]
    public class Review
    {
        [Key]
        [Column("id", TypeName = "int")]
        public int id { get; set; }
        [Required(ErrorMessage = "- Enter product id")]
        [Column("id_sanpham", TypeName = "varchar(10)")]
        public string id_sanpham { get; set; }
        [Required(ErrorMessage = "- Enter username")]
        [Column("username", TypeName = "varchar(255)")]
        public string username { get; set; }
        [Required(ErrorMessage = "- Enter rating")]
        [Column("sosao", TypeName = "int")]
        public int sosao { get; set; }

        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter comment content")]
        [Column("noidung", TypeName = "text")]
        public string noidung { get; set; }
    }
}