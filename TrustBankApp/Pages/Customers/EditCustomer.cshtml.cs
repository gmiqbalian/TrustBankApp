using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.Customers
{
    public class EditCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public EditCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public EditCustomerViewModel EditCustomerViewModel { get; set; }

        public void OnGet(int customerId)
        {
            var customerToEdit = _customerService.GetCustomerById(customerId);

            EditCustomerViewModel.CustomerId = customerToEdit.CustomerId;
            EditCustomerViewModel.GivenName = customerToEdit.Givenname;

        }
    }
}
