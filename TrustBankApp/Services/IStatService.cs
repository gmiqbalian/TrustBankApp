using TrustBankApp.Models;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Services
{
    public interface IStatService
    {
        int GetCustomersCountByCountry(string countryName);
        int GetAccountsCountByCountry(string countryName);
        decimal GetCapitalCountByCountry(string countryName);
        List<TopTenAccountsViewModel> GetTopTenAccountsByCountry(string countryName);
    }
}
