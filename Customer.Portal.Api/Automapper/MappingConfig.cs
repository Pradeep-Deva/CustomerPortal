using AutoMapper;
using Customer.Portal.Api.ViewModels;
using Customer.Portal.DataAccess.Read.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.Api.Automapper
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<CustomerOrderDetails, CustomerOrderDetailsInputViewModel>();
        }
    }
}
