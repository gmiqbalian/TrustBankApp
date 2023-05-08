using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.Customer;

namespace TrustBankApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
    public class EditCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public EditCustomerModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public EditCustomerViewModel EditCustomerVM { get; set; } = new EditCustomerViewModel();

        public void OnGet(int customerId)
        {
            EditCustomerVM.GendersDropDownList = _customerService.FillGenderDropDownList();
            EditCustomerVM.CountriesDropDownList = _customerService.FillCountryDropDownList();

            var customerToEdit = _customerService.GetCustomerById(customerId);

            _mapper.Map(customerToEdit, EditCustomerVM);
        }
        public IActionResult OnPost(int customerId)
        {
            EditCustomerVM.GendersDropDownList = _customerService.FillGenderDropDownList();
            EditCustomerVM.CountriesDropDownList = _customerService.FillCountryDropDownList();

            if (ModelState.IsValid)
            {
                EditCustomerVM.CustomerId = customerId;
                _customerService.EditCustomer(EditCustomerVM);

                return RedirectToPage("/Customers/Customer", new { customerId = customerId });
            }

            return Page();
        }
    }
}
