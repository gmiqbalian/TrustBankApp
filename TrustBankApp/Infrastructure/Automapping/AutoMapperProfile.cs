using AutoMapper;
using TrustBankApp.Models;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Infrastructure.Automapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<NewCustomerViewModel, Customer>()
                .ReverseMap();
            CreateMap<EditCustomerViewModel, Customer>()
                .ReverseMap();
        }
    }
}
