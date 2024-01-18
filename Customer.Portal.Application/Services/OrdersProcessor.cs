
using Customer.Portal.Application.Contracts; 
using Customer.Portal.DataAccess.Read.Contracts;
using Customer.Portal.DataAccess.Read.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.Application.Services
{
    public class OrdersProcessor : IOrdersProcessor
    {
        #region PRIVATE VARIABLES
        private readonly ICustomerPortalReadRepository _customerPortalReadRepository;
        #endregion

        #region CONSTRUCTOR
        public OrdersProcessor(ICustomerPortalReadRepository customerPortalReadRepository)
        {
            _customerPortalReadRepository = customerPortalReadRepository;
        }
        #endregion

        #region PUBLIC METHODS
        public async Task<CustomerOrderDetails> GetRecentOrderDetails(string userEmail, string customerId)
        { 
            var customerDetails = await _customerPortalReadRepository.GetCustomerDetails(userEmail, customerId); //This method will get the customer details
            if (customerDetails is null)
                throw new InvalidOperationException();  
            var lastOrderBasicDetails = await _customerPortalReadRepository.GetLastOrderBasicDetails(customerId); //This method will get the last purchase basic details
            var lastOrderProductDetails= lastOrderBasicDetails is null ? null : await _customerPortalReadRepository.GetLastOrderProductDetails(lastOrderBasicDetails.OrderNumber.ToString()); //This method will get the list of last purchase Product Item details
            return MapOrderDetails(customerDetails, lastOrderBasicDetails, lastOrderProductDetails);
        }
        #endregion

        #region PRIVATE METHODS
        private IEnumerable<OrderItems> MapOrderItems(IEnumerable<OrderItems> lastOrderProductDetails)
        {
           var allOrderItems= new List<OrderItems>(); 
            foreach (var p in lastOrderProductDetails)
            {
                var orderItems = new OrderItems();
                orderItems.Product = p.Product;
                orderItems.Quantity = p.Quantity;
                orderItems.PriceEach = p.PriceEach;
                allOrderItems.Add(orderItems);
            }
            return allOrderItems;
        }
        private string ConvertDatetoRequiredFormat(string oldFormat)
        {
            DateTime dateTime = DateTime.ParseExact(oldFormat, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            string newFormat = dateTime.ToString("dd-MMM-yyyy");
            return newFormat;
        }

        private CustomerOrderDetails MapOrderDetails(CustomerDetails customerDetails, OrderDetails lastOrderBasicDetails, IEnumerable<OrderItems> lastOrderProductDetails)
        {

            CustomerDetails customerData = new CustomerDetails()
            {
                FirstName = customerDetails.FirstName,
                LastName = customerDetails.LastName
            };
            OrderDetails? orderDetails = lastOrderBasicDetails is null ? null: new OrderDetails()
            {
                OrderNumber = lastOrderBasicDetails.OrderNumber,
                OrderDate = ConvertDatetoRequiredFormat(lastOrderBasicDetails.OrderDate),
                DeliveryAddress = lastOrderBasicDetails.DeliveryAddress,
                OrderItems = MapOrderItems(lastOrderProductDetails),
                DeliveryExpected = ConvertDatetoRequiredFormat(lastOrderBasicDetails.DeliveryExpected) 
            };

            CustomerOrderDetails result = new CustomerOrderDetails()
            {
                Customer = customerData,
                Order= orderDetails
            };
             

            return result;
        }
        #endregion

    }

}
