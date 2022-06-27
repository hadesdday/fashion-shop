using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models.Dao
{
    //private static Dictionary<string,Product> listProduct ;
    public class CartDao
    {
        private Product pro = new Product();
        private String id ;

        public CartDao(string id, Product pro)
        {
            this.Pro = pro;
            this.Id = id;
        }

        public Product Pro { get => pro; set => pro = value; }
        public string Id { get => id; set => id = value; }

    }
}