using System;
using System.Collections.Generic;

namespace fashion_shop_group32.Models
{
    public class ViewModelIndex2
    {
        public Product product { get; set; }
        public IEnumerable<string> list1 { get; set; }
        public IEnumerable<string> list2 { get; set; }
        public IEnumerable<Product> list3 { get; set; }
        public IEnumerable<Review> fourFirstComments { get; set; }
        public IEnumerable<Review> remainComments { get; set; }
        public int commentQuantity { get; set; }
        public bool isRemainingComment { get; set; }
    }
}