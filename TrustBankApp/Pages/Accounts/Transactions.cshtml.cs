using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static TrustBankApp.Services.AccountService;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.Accounts
{

    public class TransactionsModel : PageModel
    {
        private readonly IAccountService _accountService;

        public TransactionsModel(IAccountService accountService)
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
        public PagedResult<TransactionViewModel> Transactions { get; set; }

        public void OnGet(int accountId, string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            if (pageNo <= 0)
                pageNo = 1;

            SortColumn = sortColumn; 
            SortOrder = sortOrder; 
            SearchText = searchText;
            CurrentPageNumber = pageNo;

            AccountId = accountId;

            Transactions = _accountService.GetAllTransactions(accountId, sortColumn, sortOrder, pageNo, searchText);
        }
    }
}
