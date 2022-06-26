using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    [Table("sanpham")]
    public class ProductEntity
    {
        [Key]
        [Column("id_sanpham", TypeName = "varchar(50)")]
        public string id_sanpham { get; set; }
        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter product name")]
        [Column("ten_sp", TypeName = "varchar(50)")]
        public string ten_sp { get; set; }
        [Required(ErrorMessage = "- Enter product type")]
        [Column("ma_loaisp", TypeName = "varchar(50)")]
        public string ma_loaisp { get; set; }
        [Required(ErrorMessage = "- Enter price")]
        [Column("gia", TypeName = "double")]
        public double gia { get; set; }
        [Column("id_km", TypeName = "varchar(50)")]
        public string id_km { get; set; }
        [Required(ErrorMessage = "- Enter color type")]
        [Column("ma_mau", TypeName = "varchar(50)")]
        public string ma_mau { get; set; }
        [Required(ErrorMessage = "- Enter size type")]
        [Column("ma_size", TypeName = "varchar(50)")]
        public string ma_size { get; set; }
        [Required(ErrorMessage = "- Enter brand name")]
        [Column("thuonghieu", TypeName = "varchar(50)")]
        public string thuonghieu { get; set; }
        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter type")]
        [Column("loai", TypeName = "varchar(50)")]
        public string loai { get; set; }
        [Required(ErrorMessage = "- Enter in-store quantity")]
        [Column("soluongton", TypeName = "int")]
        public int soluongton { get; set; }
        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter product description")]
        [Column("mota", TypeName = "text")]
        public string mota { get; set; }
        [Required(ErrorMessage = "- Enter product status")]
        [Column("active", TypeName = "tinyint")]
        public int active { get; set; }
    }
}