using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TrustBankApp.Models;
using TrustBankApp.Pages.TopTenAccounts;
using TrustBankApp.ViewModels;
using TrustBankApp.ViewModels.AccountsVM;
using TrustBankApp.ViewModels.Customer;
using TrustBankApp.ViewModels.User;

namespace TrustBankApp.Infrastructure.Automapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //Customers
            CreateMap<NewCustomerViewModel, Customer>()
                .ReverseMap();

            CreateMap<EditCustomerViewModel, Customer>()
                .ReverseMap();

            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.FullName, opt=> opt.MapFrom(src => src.Givenname + " " + src.Surname))
                .ReverseMap();

            CreateMap<Customer, CustomerDetailViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Givenname + " " + src.Surname))
                .ReverseMap();

            //Users
            CreateMap<IdentityUser, EditUserViewModel>();


            //Accounts
            CreateMap<Account, AccountDetailViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ReverseMap();

            CreateMap<Transaction, TransactionViewModel>()
                .ReverseMap();

            //Stats
            CreateMap<Disposition, TopTenAccountsViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Customer.Givenname + " " + src.Customer.Surname ))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.CustomerId))
                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.Customer.NationalId))
                .ForMember(dest => dest.Emailaddress, opt => opt.MapFrom(src => src.Customer.Emailaddress))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Account.Balance))
                .ReverseMap();

        }
    }
}
