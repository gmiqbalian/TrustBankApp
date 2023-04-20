using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using static TrustBankApp.Pages.CustomersModel;

namespace TrustBankApp.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly TrustBankDbContext _dbContext;

        public CustomerModel(TrustBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public class CustomerDetailViewModel
        {
            public int CustomerId { get; set; }
            public string NationalId { get; set; }
            public int AccountId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName { get; set; }
            public string Gender { get; set; }
            public DateTime Birthday { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
            public string Region { get; set; }
            public string Country { get; set; }
            public string CountryCode { get; set; }
            public string TelephoneCountryCode { get; set; }
            public string TelephoneNumber { get; set; }
            public List<Account> Accounts { get; set; }
            public decimal TotalBalance { get; set; }
            public int Age { get; set; }


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
            Customer.FirstName = customerToShow.Givenname;
            Customer.LastName = customerToShow.Surname;
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
        }
    }
}
