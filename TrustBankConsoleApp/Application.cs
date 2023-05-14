using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBankApp.Models;
using TrustBankConsoleApp.Models;
using TrustBankConsoleApp.Services;

namespace TrustBankConsoleApp
{
    internal class Application
    {
        private readonly IMoneyLaunderingService _moneyLaunderingService;

        public Application(IMoneyLaunderingService moneyLaunderingService)
        {
            _moneyLaunderingService = moneyLaunderingService;
        }
        public void Run()
        {
            var reportingDate = File.ReadAllLines("../../../Laundering Reports/LastReportDate.txt").FirstOrDefault();
            
            DateTime checkedUpto;
            checkedUpto = DateTime.Parse(reportingDate);
            

            var countriesToCheck = new[] { "Sweden", "Norway", "Denmark", "Finland" };
            
            foreach(var country in countriesToCheck)
            {
                var forCustomers = _moneyLaunderingService.GetAllCustomersByCountry(country, checkedUpto);

                var singleMoneyLaunderingRecords = _moneyLaunderingService.GetSingleMoneyLaunderingRecords(forCustomers);
                _moneyLaunderingService.GenerateMoneyLaunderinReport(singleMoneyLaunderingRecords, country, "Single");

                var moneyLaunderingRecords72h = _moneyLaunderingService.Get72hMoneyLaunderingRecords(forCustomers);
                _moneyLaunderingService.GenerateMoneyLaunderinReport(moneyLaunderingRecords72h, country, "72h");
            }
         
            File.WriteAllText("../../../Laundering Reports/LastReportDate.txt", DateTime.Now.ToString());
        }

    }
}
