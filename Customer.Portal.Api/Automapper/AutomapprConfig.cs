using AutoMapper;
using Customer.Portal.Api.ViewModels;
using Customer.Portal.DataAccess.Read.Models; 

namespace Customer.Portal.Api.Automapper
{
    public class AutomapprConfig: Profile
    {

        public AutomapprConfig()
        {

            CreateMap<DataAccess.Read.Models.CustomerOrderDetails, CustomerRecentOrderOutputViewModel>().ReverseMap();
            CreateMap<DataAccess.Read.Models.OrderDetails, ViewModels.OrderDetails>().ReverseMap(); 
            CreateMap<DataAccess.Read.Models.CustomerDetails, ViewModels.CustomerDetails>().ReverseMap();
            CreateMap<DataAccess.Read.Models.OrderItems, ViewModels.OrderItems>().ReverseMap();





        }
    }
}
