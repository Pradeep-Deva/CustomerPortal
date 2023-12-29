using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.DataAccess.Read.Models
{
    public class CustomerOrderDetails
    {
        public CustomerDetails Customer { get; set; }
        public OrderDetails? Order { get; set; }
    }
    public class CustomerDetails
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class OrderDetails
    {

        public int OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; }
        public string DeliveryExpected { get; set; }
    }

    public class OrderItems
    {

        public string Product { get; set; }
        public int Quantity { get; set; }
        public int PriceEach { get; set; }
    }
}
