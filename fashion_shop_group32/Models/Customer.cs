using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    [Table("khachhang")]
    public class Customer
    {
        [Key]
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