using Customer.Portal.DataAccess.Read.Constants;
using Customer.Portal.DataAccess.Read.Contracts;
using Customer.Portal.DataAccess.Read.Models;
using Customer.Portal.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.DataAccess.Read.CustomerPortalRepo
{
    public class CustomerPortalReadRepository: CustomerPortalBaseRepository, ICustomerPortalReadRepository
    {
        #region CONSTRUCTOR
        public CustomerPortalReadRepository(IConnectionFactory ConnectionFactory) : base(ConnectionFactory)
        {

        }
        #endregion

        #region PUBLIC METHODS
        public async Task<Models.CustomerDetails> GetCustomerDetails(string emailId, string customerId)
        {
            return await SelectFirstOrDefaultAsync<Models.CustomerDetails>(PurchaseQueries.GET_CUSTOMER_DETAILS, new { EmailId = emailId, CustomerId = customerId });
        }

        public async Task<Models.OrderDetails> GetLastOrderBasicDetails(string customerId)
        {
            return await SelectFirstOrDefaultAsync<Models.OrderDetails>(PurchaseQueries.GET_LASTORDER_BASIC_DETAILS, new {CustomerId = customerId });
        }

        public async Task<IEnumerable<Models.OrderItems>> GetLastOrderProductDetails(string orderId)
        {
            return await SelectAsync<Models.OrderItems>(PurchaseQueries.GET_LASTORDER_PRODUCT_DETAILS, new { OrderId = orderId });
        }
        #endregion
    }

}
