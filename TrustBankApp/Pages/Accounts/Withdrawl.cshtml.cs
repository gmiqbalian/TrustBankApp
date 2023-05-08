using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.AccountsVM;

namespace TrustBankApp.Pages.Accounts
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
    public class WithdrawlModel : PageModel
    {
        private readonly IAccountService _accountService;

        public WithdrawlModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public WithdrawViewModel WithdrawVM { get; set; } = new WithdrawViewModel();
        public void OnGet(int customerId, int accountId)
        {
            WithdrawVM.CustomerId = customerId;
            WithdrawVM.AccountId = accountId;
            WithdrawVM.Balance = _accountService
                .GetAccountById(accountId).Balance;
        }
        public IActionResult OnPost(int customerId, int accountId) //control that customer do not withdraw more than the balance
        {
            var account = _accountService.GetAccountById(accountId);

            WithdrawVM.CustomerId = customerId;
            WithdrawVM.AccountId = accountId;
            WithdrawVM.Balance = _accountService
                .GetAccountById(accountId).Balance;
            
            if (WithdrawVM.Balance < WithdrawVM.Amount)
            {
                ModelState.AddModelError("WithdrawVM.Amount", "There is not enough balance in the account");
            }

            if (ModelState.IsValid)
            {
                _accountService.MakeWithdrawl(WithdrawVM);
                return RedirectToPage("/Customers/Customer", new { customerId = customerId });
            }

            return Page();
        }
    }
}
