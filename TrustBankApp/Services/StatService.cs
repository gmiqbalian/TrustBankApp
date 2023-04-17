using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;

namespace TrustBankApp.Services
{
    public class StatService : IStatService
    {
        private readonly TrustBankDbContext _dbContext;
        private readonly string _countryName;

        public StatService(TrustBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public int GetCustomersCountByCountry(string countryName)
        {
            return _dbContext.Customers.Where(c => c.Country == countryName).Count();
        }
        public int GetAccountsCountByCountry(string countryName)
        {
            return _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(c => c.Account)
                .Where(c => c.Country == countryName)
                .SelectMany(c => c.Dispositions.Where(d => d.Type == "Owner")).Count();              
        }
        public decimal GetCapitalCountByCountry(string countryName)
        {
            return _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .Where(c => c.Country == countryName)
                .SelectMany(c => c.Dispositions)
                .Sum(c => c.Account.Balance);
        }
    }
}
