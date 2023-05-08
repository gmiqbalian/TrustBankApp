using AutoMapper;
using TrustBankApp.Models;
using TrustBankApp.ViewModels.AccountsVM;
using TrustBankApp.ViewModels.Customer;

namespace TrustBankAPI.Infrastructure.Automapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDetailViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Givenname + " " + src.Surname))
                .ReverseMap();

            CreateMap<Transaction, TransactionViewModel>()
                .ReverseMap();
        }
    }
}
