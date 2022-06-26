using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    public class Order
    {
        [Key]
        [Column("id_hoadon", TypeName = "int")]
        public int id_hoadon { get; set; }
        [Required(ErrorMessage = "- Enter customer id")]
        [Column("id_khachHang", TypeName = "int")]
        public int id_khachHang { get; set; }
        [Column("id_magg", TypeName = "varchar(10)")]
        public string id_magg { get; set; }
        [Required(ErrorMessage = "- Enter payment method ")]
        [Column("mapttt", TypeName = "varchar(10)")]
        public string mapttt { get; set; }
        [Required(ErrorMessage = "- Enter price")]
        [Column("trigia", TypeName = "double")]
        public double trigia { get; set; }
        [Required(ErrorMessage = "- Enter status")]
        [Column("trangthai", TypeName = "tinyint")]
        public int trangthai { get; set; }
        [Column("createdat", TypeName = "timestamp")]
        public string createdat { get; set; }
    }
}