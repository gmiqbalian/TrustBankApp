using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static TrustBankApp.Services.AccountService;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;
using TrustBankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TrustBankApp.Pages.Accounts
{

    public class TransactionsModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly TrustBankDbContext _dbContext;

        public TransactionsModel(IAccountService accountService, TrustBankDbContext dbContext)
        {
            _accountService = accountService;
            _dbContext = dbContext;
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
            TotalPages = Transactions.TotalPages;
        }
        public IActionResult OnGetShowMore(int pageNo, int accountId)
        {
            var account = _dbContext.Accounts
                //.Include(x => x.Transactions)
                .Where(x => x.AccountId == accountId)
                .SelectMany(x => x.Transactions)
                .GetPaged(pageNo, 20)
                .Results.Select(x => new TransactionViewModel
                {
                    TransactionId = x.TransactionId,
                    Date = x.Date,
                    Amount = x.Amount,
                    Balance = x.Balance,
                    Type = x.Type
                });

            return new JsonResult(new { resultList = account });
        }
    }
}
    