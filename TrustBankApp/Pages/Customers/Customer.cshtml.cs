using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.Customer;

namespace TrustBankApp.Pages
{
    [Authorize(Roles = "Cashier")]
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CustomerModel(ICustomerService customerService, IAccountService accountService, IMapper mapper)
        {
            _customerService = customerService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public CustomerDetailViewModel CustomerVM { get; set; } = new CustomerDetailViewModel();
        public void OnGet(int customerId)
        {
            var customerToShow = _customerService.GetCustomerById(customerId);
            
            _mapper.Map(customerToShow, CustomerVM);

            CustomerVM.Accounts = _accountService.GetCustomerAccounts(customerId);
            CustomerVM.TotalBalance = _accountService.GetCustomerAccountsBalance(customerId);
        }

    }
}
