using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.AccountsVM;

namespace TrustBankApp.Pages.Accounts
{
    [Authorize (Roles = "Cashier")]
    public class AccountsModel : PageModel
    {
        private readonly IAccountService _accountService;

        public AccountsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string SearchText { get; set; }
        public int CurrentPageNumber { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TotalPages { get; set; }
        public int AccountId { get; set; }
        public PagedResult<AccountDetailViewModel> Accounts { get; set; }
        public void OnGet(string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            if(pageNo == 0)            
                pageNo = 1;

            SortColumn = sortColumn;
            SortOrder = SortOrder;
            CurrentPageNumber = pageNo;
            SearchText = searchText;

            Accounts = _accountService.GetAllAccounts(sortColumn, sortOrder, pageNo, searchText);
            
            TotalPages = Accounts.TotalPages;
        }
        public IActionResult OnGetFetchInfo(int accountId)
        {
            var account = _accountService.GetOwnerOfAccount(accountId);
            
            return new JsonResult(new
            {
                customerId = account.CustomerId,
                fullName = account.Givenname + " " + account.Surname,
                country = account.Country,
                email = account.Emailaddress
            });
        }
    }
}
