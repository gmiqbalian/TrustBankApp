using TrustBankApp.Models;

namespace TrustBankApp.Services
{
    public interface IStatService
    {
        int GetCustomersCountByCountry(string countryName);
        int GetAccountsCountByCountry(string countryName);
        decimal GetCapitalCountByCountry(string countryName);
    }
}
