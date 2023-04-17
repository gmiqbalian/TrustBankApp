using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using static TrustBankApp.Pages.IndexModel;

namespace TrustBankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TrustBankDbContext _dbContext;
        public IndexModel(ILogger<IndexModel> logger, TrustBankDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
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
            StatsSweden.Clients = _dbContext.Customers
                .Where(c => c.Country == "Sweden").Count();
            StatsSweden.Accounts = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(c => c.Account)
                .Where(c => c.Country == "Sweden")
                .SelectMany(c => c.Dispositions.Where(d => d.Type == "Owner"))
                .Count();
            StatsSweden.Capital = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .Where(c => c.Country == "Sweden")
                .SelectMany(c => c.Dispositions)
                .Sum(c => c.Account.Balance);

            StatsNorway.Clients = _dbContext.Customers
                .Where(c => c.Country == "Norway").Count();
            StatsNorway.Accounts = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(c => c.Account)
                .Where(c => c.Country == "Norway")
                .SelectMany(c => c.Dispositions.Where(d => d.Type == "Owner"))
                .Count();
            StatsNorway.Capital = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .Where(c => c.Country == "Norway")
                .SelectMany(c => c.Dispositions)
                .Sum(c => c.Account.Balance);

            StatsFinland.Clients = _dbContext.Customers
                .Where(c => c.Country == "Finland").Count();
            StatsFinland.Accounts = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(c => c.Account)
                .Where(c => c.Country == "Finland")
                .SelectMany(c => c.Dispositions.Where(d => d.Type == "Owner"))
                .Count();
            StatsFinland.Capital = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .Where(c => c.Country == "Finland")
                .SelectMany(c => c.Dispositions)
                .Sum(c => c.Account.Balance);

            StatsDenmark.Clients = _dbContext.Customers
                .Where(c => c.Country == "Denmark").Count();
            StatsDenmark.Accounts = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(c => c.Account)
                .Where(c => c.Country == "Denmark")
                .SelectMany(c => c.Dispositions.Where(d => d.Type == "Owner"))
                .Count();
            StatsDenmark.Capital = _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .Where(c => c.Country == "Denmark")
                .SelectMany(c => c.Dispositions)
                .Sum(c => c.Account.Balance);
        }
    }
}