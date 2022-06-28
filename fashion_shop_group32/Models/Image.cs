using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fashion_shop_group32.Models
{
    public class Image
    {
        [Key]
        [Column("id_anh", TypeName = "int")]
        public int id_anh { get; set; }

        [Required(ErrorMessage = "- Enter image url")]
        [Column("link_anh", TypeName = "varchar(255)")]
        public string link_anh { get; set; }

        [Required(ErrorMessage = "- Enter color name")]
        [Column("id_sanpham", TypeName = "varchar(50)")]
        public string id_sanpham { get; set; }
    }
}