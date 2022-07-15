using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    public class OrderDetailsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "int")]
        public int id { get; set; }
        [Required(ErrorMessage = "- Enter order id")]
        [Column("id_hoadon", TypeName = "int")]
        public int id_hoadon { get; set; }
        [Required(ErrorMessage = "- Enter product id")]
        [Column("id_sanpham", TypeName = "varchar(50)")]
        public string id_sanpham { get; set; }
        [Required(ErrorMessage = "- Enter quantity")]
        [Column("soluong", TypeName = "int")]
        public int soluong { get; set; }
        [Required(ErrorMessage = "Required")]
        [Column("ma_mau", TypeName = "varchar(15)")]
        public string ma_mau { get; set; }
        [Required(ErrorMessage = "Required")]
        [Column("ma_size", TypeName = "varchar(10)")]
        public string ma_size { get; set; }
    }
}