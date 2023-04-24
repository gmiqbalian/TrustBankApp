using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.Customers
{
    [BindProperties]
    public class EditCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly TrustBankDbContext _dbContext;

        public EditCustomerModel(ICustomerService customerService, TrustBankDbContext dbContext)
        {
            _customerService = customerService;
            _dbContext = dbContext;
        }
        public EditCustomerViewModel EditCustomerViewModel { get; set; } = new EditCustomerViewModel();

        public void OnGet(int customerId)
        {
            EditCustomerViewModel.GendersDropDownList = _customerService.FillGenderDropDownList();
            EditCustomerViewModel.CountriesDropDownList = _customerService.FillCountryDropDownList();


            var customerToEdit = _customerService.GetCustomerById(customerId);

            EditCustomerViewModel.CustomerId = customerToEdit.CustomerId;
            EditCustomerViewModel.NationalId = customerToEdit.NationalId;
            EditCustomerViewModel.GivenName = customerToEdit.Givenname;
            EditCustomerViewModel.SurName = customerToEdit.Surname;
            EditCustomerViewModel.Gender = customerToEdit.Gender;
            EditCustomerViewModel.Birthday = Convert.ToDateTime(customerToEdit.Birthday);
            EditCustomerViewModel.StreetAddress = customerToEdit.Streetaddress;
            EditCustomerViewModel.Email = customerToEdit.Emailaddress;
            EditCustomerViewModel.TelephoneNumber = customerToEdit.Telephonenumber;
            EditCustomerViewModel.City = customerToEdit.City;
            EditCustomerViewModel.Country = customerToEdit.Country;
            EditCustomerViewModel.ZipCode = customerToEdit.Zipcode;
        }
        public IActionResult OnPost(int customerId)
        {
            var customerToEdit = _customerService.GetCustomerById(customerId);

            customerToEdit.NationalId = EditCustomerViewModel.NationalId;
            customerToEdit.Givenname = EditCustomerViewModel.GivenName;
            customerToEdit.Surname = EditCustomerViewModel.SurName;
            customerToEdit.Birthday = EditCustomerViewModel.Birthday;
            customerToEdit.Streetaddress = EditCustomerViewModel.StreetAddress;
            customerToEdit.Emailaddress = EditCustomerViewModel.Email;
            customerToEdit.Telephonenumber = EditCustomerViewModel.TelephoneNumber;
            customerToEdit.City = EditCustomerViewModel.City;
            customerToEdit.Country = EditCustomerViewModel.Country;
            customerToEdit.Zipcode = EditCustomerViewModel.ZipCode;

            if(ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                
                return RedirectToPage("/Customers/Customers");
            }

            return Page();
        }
    }
}
