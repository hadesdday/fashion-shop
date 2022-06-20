using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models
{
    public class ViewModelIndex2
    {
        public Product product { get; set; }
        public IEnumerable<string> list1 { get; set; }
        public IEnumerable<string> list2 { get; set; }
    }
}