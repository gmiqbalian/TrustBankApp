using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages
{
    [Authorize(Roles = "Cashier")]
    public class CustomerModel : PageModel
    {
        private readonly TrustBankDbContext _dbContext;
        private readonly ICustomerService _customerService;

        public CustomerModel(TrustBankDbContext dbContext, ICustomerService customerService)
        {
            _dbContext = dbContext;
            _customerService = customerService;
        }

        public CustomerDetailViewModel CustomerVM { get; set; } = new CustomerDetailViewModel();
        public void OnGet(int customerId)
        {
            var customerToShow = _dbContext.Customers
                .First(c => c.CustomerId == customerId);

            var customerAccounts = _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Select(d => d.Account)
                .ToList();

            CustomerVM.CustomerId = customerToShow.CustomerId;
            CustomerVM.NationalId = customerToShow.NationalId;
            CustomerVM.FullName = customerToShow.Givenname + " " + customerToShow.Surname;
            CustomerVM.Emailaddress = customerToShow.Emailaddress;
            CustomerVM.Address = customerToShow.Streetaddress;
            CustomerVM.City = customerToShow.City;
            CustomerVM.Country = customerToShow.Country;
            CustomerVM.CountryCode = customerToShow.CountryCode;
            CustomerVM.Gender = customerToShow.Gender;
            CustomerVM.Zipcode = customerToShow.Zipcode;
            CustomerVM.Telephonecountrycode = customerToShow.Telephonecountrycode;
            CustomerVM.Telephonenumber = customerToShow.Telephonenumber;
            CustomerVM.Accounts = customerAccounts;
            CustomerVM.TotalBalance = CustomerVM.Accounts.Select(a => a.Balance).Sum();
            CustomerVM.Birthday = Convert.ToDateTime(customerToShow.Birthday);
            CustomerVM.PhoneNumber = customerToShow.Telephonecountrycode + 
                customerToShow.Telephonenumber;
            //Customer.Age = DateTime.Now - customerToShow.Birthday;
            CustomerVM.AccountId = customerAccounts.Select(x => x.AccountId).First();
        }

    }
}
