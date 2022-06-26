using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    [Table("thanhtoan")]
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("mapttt", TypeName = "varchar(10)")]
        public string mapttt { get; set; }

        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter payment method name")]
        [Column("tenpttt", TypeName = "varchar(255)")]
        public string tenpttt { get; set; }

        [Column("createdat", TypeName = "timestamp")]
        public string createdat { get; set; }
    }
}