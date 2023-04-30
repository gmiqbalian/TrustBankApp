using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages.TopTenAccounts
{
    //[Authorize(Roles = "Cashier")]
    [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "countryName" })]
    public class TopTenAccountsModel : PageModel
    {
        private readonly IStatService _statService;

        public TopTenAccountsModel(IStatService statService)
        {
            _statService = statService;
        }
        public List<TopTenAccountsViewModel> TopTenAccounts { get; set; }
        public string Country { get; set; }
        public void OnGet(string countryName)
        {
            Country = countryName;
            TopTenAccounts = _statService.GetTopTenAccountsByCountry(countryName);
        }
    }
}
