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
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        public TransferModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public TransferViewModel TransferVM { get; set; } = new TransferViewModel();
        public void OnGet(int customerId, int accountId)
        {
            TransferVM.CustomerId = customerId;
            TransferVM.FromAccountId = accountId;
            TransferVM.FromAccountBalance = _accountService
                .GetAccountById(accountId).Balance;
        }
        public IActionResult OnPost(int customerId, int accountId)
        {
            TransferVM.CustomerId = customerId;
            TransferVM.FromAccountId = accountId;
            TransferVM.FromAccountBalance = _accountService
                .GetAccountById(accountId).Balance;

            var toAccount = _accountService.GetAccountById(TransferVM.ToAccountId);

            if (toAccount == null)
            { 
                ModelState.AddModelError("TransferVM.ToAccountId", "Account not found.");
            }

            if (TransferVM.FromAccountBalance < TransferVM.Amount)
            {
                ModelState.AddModelError("TransferVM.Amount", "Current balance is not enough for this transaction.");
            }

            if (ModelState.IsValid)
            {
                _accountService.MakeTransfer(TransferVM);

                return RedirectToPage("/Customers/Customer", new { customerId = customerId });
            }

            return Page();
        }
    }
}

