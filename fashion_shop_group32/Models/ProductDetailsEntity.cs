using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models
{
    public class ProductDetailsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "int")]
        public int id { get; set; }
        [Required(ErrorMessage = "- Enter product id")]
        [Column("id_sanpham", TypeName = "varchar(50)")]
        public string id_sanpham { get; set; }
        [Column("ma_mau", TypeName = "varchar(15)")]
        public string ma_mau { get; set; }
        [Column("ma_size", TypeName = "varchar(10)")]
        public string ma_size { get; set; }
        [Column("id_anh", TypeName = "int")]
        public int id_anh { get; set; }
    }
}