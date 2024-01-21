
using AutoMapper;
using Customer.Portal.Api.ViewModels;
using Customer.Portal.Application.Contracts;
using Customer.Portal.DataAccess.Read.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRecentOrders(CustomerOrderDetailsInputViewModel input)
        {
            try
            {
                var recentOrderDetails = _ordersProcessor.GetRecentOrderDetails(input.User, input.CustomerId);
                CustomerOrderDetails recentOrderDetailss = recentOrderDetails.Result; 
                return Ok(_mapper.Map<CustomerRecentOrderOutputViewModel>(recentOrderDetailss));  
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
    
        #endregion
    }
}