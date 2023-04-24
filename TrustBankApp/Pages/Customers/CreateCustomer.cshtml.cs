using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.Customers
{
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
            NewCustomerViewModel.Birthday = DateTime.Now;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateNewCustomer(NewCustomerViewModel);
                return RedirectToPage("/Customers/Customers");
            }

            return Page();
        }
    }
}
