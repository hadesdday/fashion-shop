using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fashion_shop_group32.Models;
using Microsoft.EntityFrameworkCore;

namespace fashion_shop_group32.Context
{
    public class AdminDbContext : DbContext
    {
        public DbSet<Size> Size { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Customer> khachhang { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=.net;port=3306;user=root;password=;charset=utf8;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.ma_sizesp);
                entity.Property(e => e.ten_sizesp).IsRequired();
            });

            modelBuilder.Entity<Customer>(entity =>
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

        }
    }
}