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

        public CustomerDetailViewModel Customer { get; set; } = new CustomerDetailViewModel();
        public void OnGet(int customerId)
        {
            var customerToShow = _dbContext.Customers
                .First(c => c.CustomerId == customerId);

            var customerAccounts = _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Select(d => d.Account)
                .ToList();

            Customer.CustomerId = customerToShow.CustomerId;
            Customer.NationalId = customerToShow.NationalId;
            Customer.FullName = customerToShow.Givenname + " " + customerToShow.Surname;
            Customer.Email = customerToShow.Emailaddress;
            Customer.Address = customerToShow.Streetaddress;
            Customer.City = customerToShow.City;
            Customer.Country = customerToShow.Country;
            Customer.CountryCode = customerToShow.CountryCode;
            Customer.Gender = customerToShow.Gender;
            Customer.ZipCode = customerToShow.Zipcode;
            Customer.TelephoneCountryCode = customerToShow.Telephonecountrycode;
            Customer.TelephoneNumber = customerToShow.Telephonenumber;
            Customer.Accounts = customerAccounts;
            Customer.TotalBalance = Customer.Accounts.Select(a => a.Balance).Sum();
            Customer.Birthday = Convert.ToDateTime(customerToShow.Birthday);
            Customer.PhoneNumber = customerToShow.Telephonecountrycode + 
                customerToShow.Telephonenumber;
            Customer.Age = DateTime.Now.Year - Customer.Birthday.Year;
            Customer.AccountId = customerAccounts.Select(x => x.AccountId).First();
        }

    }
}
