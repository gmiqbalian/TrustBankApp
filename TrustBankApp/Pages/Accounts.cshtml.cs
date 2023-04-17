using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;

namespace TrustBankApp.Pages
{
    public class AccountsModel : PageModel
    {
        private readonly TrustBankDbContext _dbContext;

        public AccountsModel(TrustBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class AccountViewModel 
        {
            public int AccountId { get; set; }
            public decimal Balance { get; set; }
            public string Country { get; set; }
        }
        public List<AccountViewModel> accounts { get; set; }
        public void OnGet()
        {
            

        }
    }
}
