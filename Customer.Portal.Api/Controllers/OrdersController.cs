
using AutoMapper;
using Customer.Portal.Api.ViewModels;
using Customer.Portal.Application.Contracts;
using Customer.Portal.DataAccess.Read.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.Serialization; 

namespace Customer.Portal.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        #region PRIVATE VARIABLES
        private readonly IOrdersProcessor _ordersProcessor;
        private readonly IMapper _mapper;
        #endregion

        #region CONSTRUCTOR
        public OrdersController(IOrdersProcessor ordersProcessor, IMapper mapper)
        {
            _ordersProcessor = ordersProcessor;
            _mapper = mapper;
        }
        #endregion

        #region PUBLIC METHODS
        [HttpPost]
        [Route("GetRecentOrderDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRecentOrders(CustomerOrderDetailsInputViewModel input)
        {
            try
            {
                var recentOrderDetails = _ordersProcessor.GetRecentOrderDetails(input.User, input.CustomerId);
                return Ok(_mapper.Map<CustomerOrderDetailsInputViewModel>(recentOrderDetails)); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("UserDetails Not Found");
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
        #endregion

        #region PRIVATE METHODS
        private static HttpStatusCode GetPreconditionFailed()
        {
            return HttpStatusCode.PreconditionFailed;
        }

        private IEnumerable<ViewModels.OrderItems> MapOrderItems(IEnumerable<DataAccess.Read.Models.OrderItems> lastOrderProductDetails)
        {
            var allOrderItems = new List<ViewModels.OrderItems>();
            foreach (var p in lastOrderProductDetails)
            {
                var orderItems = new ViewModels.OrderItems();
                orderItems.Product = p.Product;
                orderItems.Quantity = p.Quantity;
                orderItems.PriceEach = p.PriceEach;
                allOrderItems.Add(orderItems);
            }
            return allOrderItems;
        }

        private CustomerRecentOrderOutputViewModel MapTOCustomerRecentOrderViewModel(CustomerOrderDetails recentOrderDetails)
        {
            CustomerRecentOrderOutputViewModel result = new CustomerRecentOrderOutputViewModel();

            ViewModels.CustomerDetails customerData = new ViewModels.CustomerDetails()
            {
                FirstName = recentOrderDetails.Customer.FirstName,
                LastName = recentOrderDetails.Customer.LastName
            };
            ViewModels.OrderDetails? orderDetails = recentOrderDetails.Order is null ? null: new ViewModels.OrderDetails()
            {
                OrderNumber = recentOrderDetails.Order.OrderNumber,
                OrderDate = recentOrderDetails.Order.OrderDate,
                DeliveryAddress = recentOrderDetails.Order.DeliveryAddress,
                OrderItems= MapOrderItems(recentOrderDetails.Order.OrderItems),
                DeliveryExpected = recentOrderDetails.Order.DeliveryExpected
            };


            result.Customer = customerData;
            result.Order = orderDetails;
            return result;
        }
        #endregion
    }
}