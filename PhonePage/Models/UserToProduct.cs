using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Models
{
    public class UserToProduct
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }

    }
}
