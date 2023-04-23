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
        }
        public IActionResult OnPost()
        {
            _customerService.CreateNewCustomer(NewCustomerViewModel);
            return RedirectToPage("/Customers/Customers");
        }
    }
}
