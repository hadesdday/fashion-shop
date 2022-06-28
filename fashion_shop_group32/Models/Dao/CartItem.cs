using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace fashion_shop_group32.Models.Dao
{
    public class CartItem
    {

        private IDictionary<string, Product> listProduct;
        private static CartItem instance = null;
        public CartItem()
        {
            listProduct = new Dictionary<string, Product>();
        }
        public static CartItem getInstance()
        {
            if (instance == null)
            {
                instance = new CartItem();
            }
            return instance;
        }

        public void PutToCart(Product product)
        {
            listProduct.Add(product.id_sanpham, product);
        }
        public Product getProductById(string id)
        {
            listProduct.TryGetValue(id, out Product product);
            return product;
        }
        public void removeById(string id)
        {
            listProduct.Remove(id);
        }
        public void updateQuantity(string id, int productQuantity)
        {
            listProduct.TryGetValue(id, out Product product);
            product.soluongton = productQuantity;
        }
        public double totalPrice()
        {
            double price = 0;
            foreach (Product product in listProduct.Values)
            {
                price += product.getTotalMoney();
            }
            return price;
        }
        public int totalQuantitySold()
        {
            int quantity = 0;
            foreach (Product product in listProduct.Values)
            {
                quantity += product.quantitySold;
            }
            return quantity;
        }
        public Collection<Product> getListProducts()
        {
            return (Collection<Product>)listProduct.Values;
        }
        public void upQuantity(string id)
        {
            listProduct.TryGetValue(id, out Product product);
            product.quantitySold = product.quantitySold + 1;
        }
        public void Put(Product product)
        {
            if (listProduct.ContainsKey(product.id_sanpham))
            {
                upQuantity(product.id_sanpham);
            }
            else
            {
                PutToCart(product);
            }
        }
    }
}