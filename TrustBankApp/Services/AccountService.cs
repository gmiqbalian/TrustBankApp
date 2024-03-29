﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.ViewModels.AccountsVM;

namespace TrustBankApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly TrustBankDbContext _dbContext;
        private readonly IMapper _mapper;

        public AccountService(TrustBankDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Account GetNewAccount()
        {
            var newAccount = new Account();
            newAccount.Created = DateTime.Now;
            newAccount.Frequency = "Monthly";

            return newAccount;
        }
        public Disposition GetNewDisposition(Account forAccount)
        {
            var newDisposition = new Disposition();
            newDisposition.Account = forAccount;
            newDisposition.Type = "Owner";

            return newDisposition;
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
                        query = query.OrderBy(c => c.Created);
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
                query = query.Where(q => q.AccountId.ToString() == searchText);
            }

            var accountsQueryList = _mapper.Map<List<AccountDetailViewModel>>(query);

            return accountsQueryList.AsQueryable().GetPaged(pageNo, 20);
        }

        public List<TransactionViewModel> GetAllTransactions(int accountId)
        {
            var account = _dbContext.Accounts.Include(x => x.Transactions)
                .First(x => x.AccountId == accountId);

            var query = account.Transactions.AsQueryable();

            var transactionsQueryList = _mapper.Map<List<TransactionViewModel>>(query)
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.TransactionId);

            return transactionsQueryList.ToList();
        }

        public void MakeDeposit(DepositViewModel depositViewModel)
        {
            var toAccount = _dbContext.Accounts
                .First(x => x.AccountId == depositViewModel.AccountId);

            toAccount.Balance += depositViewModel.Amount;

            toAccount.Transactions.Add(new Transaction
            {
                AccountId = depositViewModel.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Deposit to account",
                Amount = depositViewModel.Amount,
                Balance = toAccount.Balance
            });

            _dbContext.SaveChanges();
        }
        public void MakeWithdrawl(WithdrawViewModel withdrawViewModel)
        {
            var fromAccount = _dbContext.Accounts
                .First(x => x.AccountId == withdrawViewModel.AccountId);
            
            fromAccount.Balance -= withdrawViewModel.Amount;

            fromAccount.Transactions.Add(new Transaction
            {
                AccountId = withdrawViewModel.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Withdrawl",
                Amount = withdrawViewModel.Amount,
                Balance = fromAccount.Balance
            });

            _dbContext.SaveChanges();
        }
        public void MakeTransfer(TransferViewModel transferViewModel)
        {
            var toAccount = GetAccountById(transferViewModel.ToAccountId);
            var fromAccount = GetAccountById(transferViewModel.FromAccountId);

            fromAccount.Balance -= transferViewModel.Amount;
            toAccount.Balance += transferViewModel.Amount;
            
            toAccount.Transactions.Add(new Transaction
            {
                AccountId = toAccount.AccountId,
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Collection from Another Bank",
                Amount = transferViewModel.Amount,
                Account = transferViewModel.FromAccountId.ToString(),
                Balance = toAccount.Balance
            });

            fromAccount.Transactions.Add(new Transaction
            {
                AccountId = fromAccount.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Transfer to Another Bank",
                Amount = transferViewModel.Amount * -1,
                Account = transferViewModel.ToAccountId.ToString(),
                Balance = fromAccount.Balance
            });


            _dbContext.SaveChanges();
        }
        public Account GetAccountById(int accountId)
        {
            return _dbContext.Accounts.Find(accountId);
        }
        public List<Account> GetCustomerAccounts(int customerId)
        {
            var customerAccounts = _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Select(d => d.Account)
                .ToList();

            return customerAccounts;
        }
        public decimal GetCustomerAccountsBalance(int customerId)
        {
            return GetCustomerAccounts(customerId).Select(x => x.Balance).Sum();
        }
        public List<Transaction> GetAllTransactionsByAccountId(int accountId)
        {
            return _dbContext.Accounts
                .Include(x => x.Transactions)
                .First(x => x.AccountId == accountId)
                .Transactions
                .ToList();
        }
        public List<Account> GetCustomerAccountsWithTransactions(int customerId)
        {
            var customerAccounts = _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Include(x => x.Account)
                .ThenInclude(x => x.Transactions)
                .Select(d => d.Account)
                .ToList();

            return customerAccounts;
        }
        public Customer GetOwnerOfAccount(int accountId)
        {
            var ownerOfAccount = _dbContext.Dispositions
                .Where(d => d.AccountId == accountId)
                .Where(d => d.Type == "Owner")
                .Select(d => d.Customer)
                .First();

            return ownerOfAccount;
        }

    }
}
