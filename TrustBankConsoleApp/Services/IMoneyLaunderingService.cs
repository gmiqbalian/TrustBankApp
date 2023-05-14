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
        List<CustomerConsoleViewModel> GetAllCustomersByCountry(string countryName, DateTime checkedUpto);
        List<LaunderingRecord> GetSingleMoneyLaunderingRecords(List<CustomerConsoleViewModel> forCustomers);
        List<LaunderingRecord> Get72hMoneyLaunderingRecords(List<CustomerConsoleViewModel> forCustomers);
        void GenerateMoneyLaunderinReport(List<LaunderingRecord> forMoneyLaunderRecords, string forCountry, string reportType);
    }
}
