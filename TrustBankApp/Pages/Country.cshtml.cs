using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using static TrustBankApp.Pages.AccountsModel;

namespace TrustBankApp.Pages
{
    public class CountryModel : PageModel
    {
        private readonly TrustBankDbContext _dbContext;

        public CountryModel(TrustBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string CountryName { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public void OnGet(string country)
        {
            CountryName = country;

            var query = _dbContext.Dispositions
                .Include(d => d.Customer)
                .Include(d => d.Account);

            Accounts = query.Where(a => a.Customer.Country == country)
                .Select(a => new AccountViewModel
            {
                AccountId = a.AccountId,
                Balance = a.Account.Balance,
                Country = a.Customer.Country,
            }).ToList();

            Accounts = Accounts.OrderByDescending(a => a.Balance).Take(5).ToList();


        }
    }
}
