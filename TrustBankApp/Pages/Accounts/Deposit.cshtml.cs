using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.Accounts
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DepositModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public DepositViewModel DepositViewModel { get; set; } = new DepositViewModel();

        public void OnGet(int accountId)
        {
            DepositViewModel.AccountId = accountId;
            DepositViewModel.Balance = _accountService
                .GetAccountById(accountId).Balance;
        }
        public IActionResult OnPost(int customerId, int accountId)
        {
            DepositViewModel.AccountId = accountId;

            if(ModelState.IsValid)
            {
                _accountService.MakeDeposit(DepositViewModel);
                return RedirectToPage("/Customers/Customer", new {customerId = customerId});
            }

            return Page();
        }
    }
}
