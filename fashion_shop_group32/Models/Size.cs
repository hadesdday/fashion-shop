using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace fashion_shop_group32.Models
{
    [Table("sizesanpham")]
    public class Size
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ma_sizesp", TypeName = "varchar(10)")]
        public string ma_sizesp { get; set; }

        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter size name")]
        [Column("ten_sizesp", TypeName = "varchar(255)")]
        public string ten_sizesp { get; set; }

        [Column("createdat", TypeName = "timestamp")]
        public string createdat { get; set; }
    }
}