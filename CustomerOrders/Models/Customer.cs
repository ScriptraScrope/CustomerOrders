using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrders.Models
{
    public class Customer
    {
        public int? Id { get; set; } //Customer_Id 
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
