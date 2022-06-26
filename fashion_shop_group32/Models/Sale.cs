using MySql.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_km", TypeName = "varchar(10)")]
        public string id_km { get; set; }

        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter sale name")]
        [Column("ten_km", TypeName = "varchar(50)")]
        public string ten_km { get; set; }

        [MySqlCharset("utf8")]
        [Required(ErrorMessage = "- Enter sale description")]
        [Column("noidung_km", TypeName = "text")]
        public string noidung_km { get; set; }

        [Required(ErrorMessage = "- Enter rate")]
        [Column("rate", TypeName = "double")]
        public double rate { get; set; }

        [Required(ErrorMessage = "- Enter active status")]
        [Column("active", TypeName = "tinyint")]
        public int active { get; set; }
    }
}