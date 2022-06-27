﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fashion_shop_group32.Models
{
    public interface IProduct
    {
        Product GetProduct(string id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string cat);
        IEnumerable<Product> GetProductsByCategoryAndLoai(string cat,string loai);
        IEnumerable<Product> GetLatestProducts();
        IEnumerable<Product> GetRandomProducts();
        IEnumerable<Product> GetMostSoldProducts();
        Product GetProductsByID(string id);
        Product GetProductsByName(string name);
        IEnumerable<Product> GetProductsByCategoryAndLoaiAndFilter(string cat, string loai, string mau, string size, string gia);
        IEnumerable<string> GetColorsByNameProduct(string name);
        IEnumerable<string> GetSizesByNameProduct(string name);
    }
}