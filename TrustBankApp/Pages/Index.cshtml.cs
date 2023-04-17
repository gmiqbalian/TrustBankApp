﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using TrustBankApp.Services;
using static TrustBankApp.Pages.IndexModel;

namespace TrustBankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStatService _statService;
        public IndexModel(ILogger<IndexModel> logger, TrustBankDbContext dbContext, IStatService statService)
        {
            _logger = logger;
            _statService = statService;
        }

        public class StatViewModel
        {
            public int Clients { get; set; }
            public int Accounts { get; set; }
            public decimal Capital { get; set; }
        }
        public StatViewModel StatsSweden { get; set; } = new StatViewModel();
        public StatViewModel StatsNorway { get; set; } = new StatViewModel();
        public StatViewModel StatsFinland { get; set; } = new StatViewModel();
        public StatViewModel StatsDenmark { get; set; } = new StatViewModel();

        public void OnGet()
        {
            StatsSweden.Clients = _statService.GetCustomersCountByCountry("Sweden");
            StatsSweden.Accounts = _statService.GetAccountsCountByCountry("Sweden");
            StatsSweden.Capital = _statService.GetCapitalCountByCountry("Sweden");

            StatsNorway.Clients = _statService.GetCustomersCountByCountry("Norway");
            StatsNorway.Accounts = _statService.GetAccountsCountByCountry("Norway");
            StatsNorway.Capital = _statService.GetCapitalCountByCountry("Norway");

            StatsFinland.Clients = _statService.GetCustomersCountByCountry("Finland");
            StatsFinland.Accounts = _statService.GetAccountsCountByCountry("Finland");
            StatsFinland.Capital = _statService.GetCapitalCountByCountry("Finland");

            StatsDenmark.Clients = _statService.GetCustomersCountByCountry("Denmark");
            StatsDenmark.Accounts = _statService.GetAccountsCountByCountry("Denmark");
            StatsDenmark.Capital = _statService.GetCapitalCountByCountry("Denmark");
        }
    }
}