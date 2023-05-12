using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.Customer;

namespace TrustBankApp.Pages
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public CustomersModel(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
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
            StartPage = Customers.StartPage;
            EndPage = Customers.EndPage;
        }
        public IActionResult OnGetFetchInfo(int customerId)
        {
            var account = _accountService.GetCustomerAccounts(customerId).First();
            
            return new JsonResult(new
            {
                accountId = account.AccountId,
                balance = account.Balance,
            });
        }
    }
}
