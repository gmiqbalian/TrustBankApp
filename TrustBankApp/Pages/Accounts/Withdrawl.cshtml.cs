using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.Accounts
{
    [BindProperties]
    public class WithdrawlModel : PageModel
    {
        private readonly IAccountService _accountService;

        public WithdrawlModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public WithdrawViewModel WithdrawViewModel { get; set; } = new WithdrawViewModel();
        public void OnGet(int customerId, int accountId)
        {
            WithdrawViewModel.CustomerId = customerId;
            WithdrawViewModel.AccountId = accountId;
            WithdrawViewModel.Balance = _accountService.GetAccountById(accountId).Balance;
        }
        public IActionResult OnPost(int customerId, int accountId) 
        {

            if (ModelState.IsValid)
            {
                _accountService.MakeWithdraw(WithdrawViewModel);

                return RedirectToPage("/Customers/Customer", new { customerId = customerId });
            }

            return Page();
        }
    }
}
