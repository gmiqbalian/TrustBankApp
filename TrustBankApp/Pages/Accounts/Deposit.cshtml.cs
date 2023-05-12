using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.AccountsVM;

namespace TrustBankApp.Pages.Accounts
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DepositModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public DepositViewModel DepositViewModel { get; set; } = new DepositViewModel();

        public void OnGet(int customerId, int accountId)
        {
            DepositViewModel.CustomerId = customerId;
            DepositViewModel.AccountId = accountId;
            DepositViewModel.Balance = _accountService
                .GetAccountById(accountId).Balance;
        }
        public IActionResult OnPost(int customerId, int accountId)
        {
            DepositViewModel.AccountId = accountId;
            DepositViewModel.CustomerId = customerId;
            DepositViewModel.Balance = _accountService
                .GetAccountById(accountId).Balance;

            if(ModelState.IsValid)
            {
                _accountService.MakeDeposit(DepositViewModel);
                TempData["success"] = "Deposited to account successfully!";
                return RedirectToPage("/Customers/Customer", new {customerId = customerId});
            }

            return Page();
        }
    }
}
