using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;
using static TrustBankApp.Pages.IndexModel;

namespace TrustBankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStatService _statService;
        public IndexModel(ILogger<IndexModel> logger, IStatService statService)
        {
            _logger = logger;
            _statService = statService;
        }
        
        public StatViewModel StatsSweden { get; set; } = new StatViewModel();
        public StatViewModel StatsNorway { get; set; } = new StatViewModel();
        public StatViewModel StatsFinland { get; set; } = new StatViewModel();
        public StatViewModel StatsDenmark { get; set; } = new StatViewModel();

        public void OnGet()
        {
            StatsSweden.Clients = _statService.GetCustomersCountByCountry("Sweden");
            StatsSweden.Accounts = _statService.GetAccountsCountByCountry("Sweden");
            StatsSweden.Capital = Math.Round(_statService.GetCapitalCountByCountry("Sweden"));

            StatsNorway.Clients = _statService.GetCustomersCountByCountry("Norway");
            StatsNorway.Accounts = _statService.GetAccountsCountByCountry("Norway");
            StatsNorway.Capital = Math.Round(_statService.GetCapitalCountByCountry("Norway"));

            StatsFinland.Clients = _statService.GetCustomersCountByCountry("Finland");
            StatsFinland.Accounts = _statService.GetAccountsCountByCountry("Finland");
            StatsFinland.Capital = Math.Round(_statService.GetCapitalCountByCountry("Finland"));

            StatsDenmark.Clients = _statService.GetCustomersCountByCountry("Denmark");
            StatsDenmark.Accounts = _statService.GetAccountsCountByCountry("Denmark");
            StatsDenmark.Capital = Math.Round(_statService.GetCapitalCountByCountry("Denmark"));
        }
    }
}