using MySql.EntityFrameworkCore.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    [Table("mausanpham")]
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_mausp", TypeName = "varchar(15)")]
        public string ma_mausp { get; set; }

        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter color name")]
        [Column("mausp", TypeName = "varchar(255)")]
        public string mausp { get; set; }

        [Column("createdat", TypeName = "timestamp")]
        public string createdat { get; set; }
    }
}