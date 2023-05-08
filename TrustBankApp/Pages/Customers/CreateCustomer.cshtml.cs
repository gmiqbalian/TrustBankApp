using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.Customer;

namespace TrustBankApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
    public class CreateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public NewCustomerViewModel NewCustomerViewModel { get; set; } = new NewCustomerViewModel();
        public void OnGet()
        {
            NewCustomerViewModel.GendersDropDownList = _customerService.FillGenderDropDownList();
            NewCustomerViewModel.CountriesDropDownList = _customerService.FillCountryDropDownList();
            NewCustomerViewModel.Birthday = new DateTime(1990, 01, 01);
        }
        public IActionResult OnPost()
        {
            NewCustomerViewModel.GendersDropDownList = _customerService.FillGenderDropDownList();
            NewCustomerViewModel.CountriesDropDownList = _customerService.FillCountryDropDownList();
            NewCustomerViewModel.Birthday = new DateTime(1990, 01, 01);

            if (ModelState.IsValid)
            {
                _customerService.CreateNewCustomer(NewCustomerViewModel);

                TempData["success"] = "New Customer created successfully!"; //use toastr notification service

                return RedirectToPage("/Customers/Customers");
            }

            return Page();
        }
    }
}