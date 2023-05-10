using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBankConsoleApp.Models;

namespace TrustBankConsoleApp.Services
{
    internal interface IMoneyLaunderingService
    {
        List<CustomerConsoleViewModel> GetAllCustomersByCountry(string countryName);
        List<LaunderingRecord> GetMoneyLaunderingRecords(List<CustomerConsoleViewModel> forCustomers, DateTime lastDate);
        void GenerateMoneyLaunderinReport(List<LaunderingRecord> forMoneyLaunderRecords, string forCountry);
    }
}
