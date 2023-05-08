using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TrustBankApp.Models;
using TrustBankApp.ViewModels.Customer;
using TrustBankApp.ViewModels.User;

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

            CreateMap<Customer, CustomerDetailViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Givenname + " " + src.Surname))
                .ReverseMap();

            CreateMap<IdentityUser, EditUserViewModel>();
        }
    }
}
