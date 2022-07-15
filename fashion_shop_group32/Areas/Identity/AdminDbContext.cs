using fashion_shop_group32.Models;
using Microsoft.EntityFrameworkCore;

namespace fashion_shop_group32.Context
{
    public class AdminDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=.net2;port=3306;user=root;password=;charset=utf8;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.ma_sizesp);
                entity.Property(e => e.ten_sizesp).IsRequired();
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.id_khachhang);
                entity.Property(e => e.ten_kh).IsRequired();
                entity.Property(e => e.diachi).IsRequired();
                entity.Property(e => e.sodt).IsRequired();
                entity.Property(e => e.email).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.username);
                entity.Property(e => e.password).IsRequired();
                entity.Property(e => e.role).IsRequired();
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.id_khachhang).IsRequired();
                entity.Property(e => e.active).IsRequired();
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.ma_mausp);
                entity.Property(e => e.mausp).IsRequired();
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.ma_loaisp);
                entity.Property(e => e.ten_loaisp).IsRequired();
            });
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.mapttt);
                entity.Property(e => e.tenpttt).IsRequired();
            });
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.username).IsRequired();
                entity.Property(e => e.id_sanpham).IsRequired();
                entity.Property(e => e.sosao).IsRequired();
                entity.Property(e => e.noidung).IsRequired();
            });
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.id_km);
                entity.Property(e => e.ten_km);
                entity.Property(e => e.noidung_km);
                entity.Property(e => e.rate);
                entity.Property(e => e.active);
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.id_hoadon);
                entity.Property(e => e.id_khachHang).IsRequired();
                entity.Property(e => e.id_magg);
                entity.Property(e => e.mapttt).IsRequired();
                entity.Property(e => e.trigia).IsRequired();
                entity.Property(e => e.trangthai).IsRequired();
            });
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(e => e.id_sanpham);
                entity.Property(e => e.ten_sp).IsRequired();
                entity.Property(e => e.ma_loaisp).IsRequired();
                entity.Property(e => e.gia).IsRequired();
                entity.Property(e => e.loai).IsRequired();
                entity.Property(e => e.id_km);
                entity.Property(e => e.thuonghieu).IsRequired();
                entity.Property(e => e.soluongton).IsRequired();
                entity.Property(e => e.mota).IsRequired();
                entity.Property(e => e.active).IsRequired();
            });
            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.id_anh);
                entity.Property(e => e.link_anh).IsRequired();
            });
            modelBuilder.Entity<ProductDetailsEntity>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id_sanpham).IsRequired();
                entity.Property(e => e.ma_mau);
                entity.Property(e => e.ma_size);
                entity.Property(e => e.id_anh);
            });
            modelBuilder.Entity<OrderDetailsEntity>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id_hoadon).IsRequired();
                entity.Property(e => e.id_sanpham).IsRequired();
                entity.Property(e => e.soluong).IsRequired();
                entity.Property(e => e.ma_mau);
                entity.Property(e => e.ma_size);
            });
        }

        public DbSet<Size> Size { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<CustomerEntity> khachhang { get; set; }
        public DbSet<Color> mausanpham { get; set; }
        public DbSet<ProductType> loaisanpham { get; set; }
        public DbSet<PaymentMethod> thanhtoan { get; set; }
        public DbSet<Review> review { get; set; }
        public DbSet<Sale> khuyenmai { get; set; }
        public DbSet<Order> hoadon { get; set; }
        public DbSet<ProductEntity> sanpham { get; set; }
        public DbSet<Image> hinhanh { get; set; }
        public DbSet<ProductDetailsEntity> chitietsanpham { get; set; }
        public DbSet<OrderDetailsEntity> chitiethoadon { get; set; }
    }
}