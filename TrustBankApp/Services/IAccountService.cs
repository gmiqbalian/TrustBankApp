﻿using static TrustBankApp.Services.AccountService;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.ViewModels;
using TrustBankApp.Models;

namespace TrustBankApp.Services
{
    public interface IAccountService
    {
        PagedResult<TransactionViewModel> GetAllTransactions(int accountId, string sortColumn, string sortOrder, int pageNo, string searchText);
        PagedResult<AccountDetailViewModel> GetAllAccounts(string sortColumn, string sortOrder, int pageNo, string searchText);
        void MakeDeposit(DepositViewModel depositViewModel);
        void MakeWithdraw(WithdrawViewModel withdrawViewModel);
        Account GetAccountById(int accountId);
    }
}