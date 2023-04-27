using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages
{
    [Authorize(Roles = "Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }       
        public PagedResult<CustomerViewModel> Customers { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string SearchText { get; set; }
        public int CurrentPageNumber { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TotalPages { get; set; }
        public void OnGet(string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            if(pageNo <= 0)            
                pageNo = 1;

            CurrentPageNumber = pageNo;
            SearchText = searchText;

            Customers = _customerService.GetCustomers(sortColumn, sortOrder, pageNo, searchText);
            
            TotalPages = Customers.TotalPages;
        }
        public IActionResult OnGetFetchInfo(int customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);

            return new JsonResult(new
            {
                accountId = customer.CustomerId,
                balance = customer.Zipcode,
            });
        }
    }
}
