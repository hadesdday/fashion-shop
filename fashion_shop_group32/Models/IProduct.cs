using System.Collections.Generic;

namespace fashion_shop_group32.Models
{
    public interface IProduct
    {
        Product GetProduct(string id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string cat);
        IEnumerable<Product> GetProductsByCategoryAndLoai(string cat, string loai);
        IEnumerable<Product> GetLatestProducts();
        IEnumerable<Product> GetRandomProducts();
        IEnumerable<Product> GetMostSoldProducts();
        Product GetProductsByID(string id);
        Product GetProductsByName(string name);
        IEnumerable<Product> GetProductsByCategoryAndLoaiAndFilter(string cat, string loai, string mau, string size, string gia, string keyword, int page);
        IEnumerable<string> GetColorsByIDProduct(string id);
        IEnumerable<string> GetSizesByIDProduct(string id);
        IEnumerable<Product> GetProductsBySearch(string keywword);
    }
}
