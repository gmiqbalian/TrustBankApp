using AutoMapper;
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
        private readonly IMapper _mapper;

        public StatService(TrustBankDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

            var list = _dbContext.Dispositions
                .AsQueryable()
                .Include(x => x.Customer)
                .Include(x => x.Account)
                .Where(x => x.Customer.Country == countryName)                
                .OrderByDescending(x => x.Account.Balance)
                .Take(10);


            return _mapper.Map<List<TopTenAccountsViewModel>>(list);
        }
    }
}
