using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    [Table("loaisanpham")]
    public class ProductType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_loaisp", TypeName = "varchar(10)")]
        public string ma_loaisp { get; set; }

        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter product type name")]
        [Column("ten_loaisp", TypeName = "varchar(255)")]
        public string ten_loaisp { get; set; }

        [Column("createdat", TypeName = "timestamp")]
        public string createdat { get; set; }
    }
}