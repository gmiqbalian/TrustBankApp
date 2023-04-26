using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.Accounts
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        public TransferModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public TransferViewModel TransferViewModel { get; set; } = new TransferViewModel();
        public void OnGet(int customerId, int accountId)
        {
            TransferViewModel.FromAccountId = accountId;
            TransferViewModel.FromAccountBalance = _accountService
                .GetAccountById(accountId).Balance;
        }
        public IActionResult OnPost(int customerId, int accountId)
        {
            TransferViewModel.FromAccountId = accountId;

            if (ModelState.IsValid)
            {
                _accountService.MakeTransfer(TransferViewModel);

                return RedirectToPage("/Customers/Customer", new {customerId = customerId});
            }

            return Page();
        }
    }
}

