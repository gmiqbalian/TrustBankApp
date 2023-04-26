using Microsoft.EntityFrameworkCore;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly TrustBankDbContext _dbContext;

        public AccountService(TrustBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<AccountDetailViewModel> GetAllAccounts(string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            var query = _dbContext.Accounts.AsQueryable();

            if (string.IsNullOrEmpty(searchText))
            {
                if (sortColumn == "accountId")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.AccountId);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.AccountId);

                if (sortColumn == "frequency")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Frequency);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Frequency);

                if (sortColumn == "date")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Created.Date.Month);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Created);

                if (sortColumn == "balance")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Balance);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Balance);
            }

            else if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(q => q.AccountId.ToString() == searchText ||
                    q.Frequency.ToString().ToLower().Contains(searchText.ToLower()) ||
                    q.Balance.ToString().Contains(searchText) ||
                    q.Created.ToString() == searchText);
            }

            var accountsQueryList = query.Select(x => new AccountDetailViewModel
            {
                AccountId = x.AccountId,
                Frequency = x.Frequency,
                DateCreated = x.Created,
                Balance = x.Balance,
            });

            return accountsQueryList.GetPaged(pageNo, 20);
        }

        public PagedResult<TransactionViewModel> GetAllTransactions(int accountId, string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            var account = _dbContext.Accounts.Include(x => x.Transactions)
                .First(x => x.AccountId == accountId);
            var query = account.Transactions.AsQueryable();

            if (string.IsNullOrEmpty(searchText))
            {
                if (sortColumn == "transactionId")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.TransactionId);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.TransactionId);

                if (sortColumn == "date")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Date);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Date);

                if (sortColumn == "type")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Type);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Type);

                if (sortColumn == "amount")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Amount);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Amount);

                if (sortColumn == "balance")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Balance);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Balance);
            }
            else if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(q => q.AccountId.ToString() == searchText ||
                    q.Date.ToString().Contains(searchText) ||
                    q.Type.ToLower().Contains(searchText) ||
                    q.Amount.ToString().Contains(searchText) ||
                    q.Balance.ToString().Contains(searchText));
            }
            
            var transactionsQueryList = query.Select(x => new TransactionViewModel
            {
                TransactionId = x.TransactionId,
                Date = x.Date,
                Type = x.Type,
                Amount = x.Amount,
                Balance = x.Balance,
            }).OrderByDescending(x => x.Date);

            return transactionsQueryList.GetPaged(pageNo, 30);
        }

        public void MakeDeposit(DepositViewModel depositViewModel)
        {
            var accountToMakeDeposit = _dbContext.Accounts
                .First(x => x.AccountId == depositViewModel.AccountId);

            accountToMakeDeposit.Balance += depositViewModel.Amount;
            _dbContext.SaveChanges();
        }

        public void MakeWithdraw(WithdrawViewModel withdrawViewModel)
        {
            var accountToMakeWithdraw = _dbContext.Accounts
                .First(x => x.AccountId == withdrawViewModel.AccountId);

            accountToMakeWithdraw.Balance -= withdrawViewModel.Amount;
            _dbContext.SaveChanges();
        }
        public Account GetAccountById(int accountId)
        {
            return _dbContext.Accounts.First(x => x.AccountId == accountId);
        }
    }
}
