using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.AccountsVM;
using TrustBankApp.ViewModels.Customer;

namespace TrustBankApp.Pages.Accounts
{
    public class AccountDetailModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountDetailModel(ICustomerService customerService, IAccountService accountService, IMapper mapper)
        {
            _customerService = customerService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public AccountDetailViewModel Account { get; set; } = new AccountDetailViewModel();
        public List<TransactionViewModel> Transactions { get; set; }
        public void OnGet(int accountId)
        {
            var accountToShow = _accountService.GetAccountById(accountId);

            _mapper.Map(accountToShow, Account);

            var transactions = _accountService.GetAllTransactionsByAccountId(accountId)
                .OrderByDescending(x => x.Date).Take(10).ToList();
                
            Transactions = transactions.Select(x => new TransactionViewModel
            {
                TransactionId = x.TransactionId,
                AccountId = x.AccountId,
                Amount = x.Amount,
                Date = x.Date,
                Balance = x.Balance
            }).ToList();
        }
        
    }
}
