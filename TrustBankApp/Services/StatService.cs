using Microsoft.EntityFrameworkCore;
using System.Linq;
using TrustBankApp.Models;
using TrustBankApp.ViewModels;

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
        public List<TopTenAccountsViewModel> GetTopTenAccountsByCountry(string countryName)
        {
            var list = _dbContext.Customers
                .AsQueryable()
                .Include(x => x.Dispositions)
                .ThenInclude(x => x.Account)
                .Where(x => x.Country == countryName)
                .SelectMany(x => x.Dispositions)
                .OrderByDescending(x => x.Account.Balance)
                .Take(10);
            
            return list.Select(x => new TopTenAccountsViewModel
                {
                    AccountId = x.AccountId,
                    CustomerId = x.CustomerId,
                    Name = x.Customer.Givenname + " " + x.Customer.Surname,
                    Email = x.Customer.Emailaddress,
                    NationalId = x.Customer.NationalId,
                    Balance = x.Account.Balance
                }).ToList();

                
        }
    }
}
