using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class Product
    {
        public int id { get; set; }
        public int productid { get; set; }
        public string src { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int totalPrice { get; set; }

        public List<UserToProduct> UserToProducts { get; set; }

    }
}
