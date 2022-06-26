using System.Collections.Generic;

namespace fashion_shop_group32.Models
{
    public class Product
    {
        public string id_sanpham { get; set; }
        public string ten_sp { get; set; }
        public string ma_loaisp { get; set; }
        public double gia { get; set; }
        public double rateDiscount { get; set; }
        public string id_km { get; set; }
        public string ma_mau { get; set; }
        public string ma_size { get; set; }
        public string thuonghieu { get; set; }
        public string loai { get; set; }
        public int soluongton { get; set; }
        public string mota { get; set; }
        public string active { get; set; }
        public int quantitySold { get; set; }
        public string imageMain { get; set; }
        public List<string> imgs { get; set; }
        public Product(string id_sanpham, string ten_sp, string ma_loaisp, string ma_mau, string ma_size, double gia, string loai, string id_km, string thuonghieu, int soluongton, string mota, string active)
        {
            this.id_sanpham = id_sanpham;
            this.ten_sp = ten_sp;
            this.ma_loaisp = ma_loaisp;
            this.gia = gia;
            this.id_km = id_km;
            this.thuonghieu = thuonghieu;
            this.soluongton = soluongton;
            this.active = active;
            this.ma_mau = ma_mau;
            this.ma_size = ma_size;
            this.mota = mota;
            this.loai = loai;
        }
    }
}