using Customer.Portal.DataAccess.Read.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.DataAccess.Read.Contracts
{
    public interface ICustomerPortalReadRepository
    {
        Task<Models.CustomerDetails> GetCustomerDetails(string emailId, string customerId);
        Task<Models.OrderDetails> GetLastOrderBasicDetails(string customerId);
        Task<IEnumerable<Models.OrderItems>> GetLastOrderProductDetails(string orderId);
    }
}
