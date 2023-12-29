
using Customer.Portal.DataAccess.Read.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.Application.Contracts
{
    public interface IOrdersProcessor
    {
        Task<CustomerOrderDetails> GetRecentOrderDetails(string UserEmail, string CustomerId);
    }
}
