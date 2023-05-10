﻿using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.ViewModels.AccountsVM;

namespace TrustBankApp.Services
{
    public interface IAccountService
    {
        Account GetNewAccount();
        Disposition GetNewDisposition(Account forAccount);
        PagedResult<TransactionViewModel> GetAllTransactions(int accountId, string sortColumn, string sortOrder, int pageNo, string searchText);
        PagedResult<AccountDetailViewModel> GetAllAccounts(string sortColumn, string sortOrder, int pageNo, string searchText);
        void MakeDeposit(DepositViewModel depositViewModel);
        void MakeWithdrawl(WithdrawViewModel withdrawViewModel);
        void MakeTransfer(TransferViewModel transferViewModel);
        Account GetAccountById(int accountId);
        List<Account> GetCustomerAccounts(int customerId);
        decimal GetCustomerAccountsBalance(int customerId);
        List<Transaction> GetAllTransactionsByAccountId(int accountId);
        List<Account> GetCustomerAccountsWithTransactions(int customerId);
    }
}
