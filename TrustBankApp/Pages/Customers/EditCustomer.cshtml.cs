using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

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
        public EditCustomerViewModel EditCustomerViewModel { get; set; } = new EditCustomerViewModel();

        public void OnGet(int customerId)
        {
            EditCustomerViewModel.GendersDropDownList = _customerService.FillGenderDropDownList();
            EditCustomerViewModel.CountriesDropDownList = _customerService.FillCountryDropDownList();

            var customerToEdit = _customerService.GetCustomerById(customerId);

            _mapper.Map(customerToEdit, EditCustomerViewModel);
        }
        public IActionResult OnPost(int customerId)
        {
            if(ModelState.IsValid)
            {
                EditCustomerViewModel.CustomerId = customerId;
                _customerService.EditCustomer(EditCustomerViewModel);

                return RedirectToPage("/Customers/Customers");
            }

            return Page();
        }
    }
}
