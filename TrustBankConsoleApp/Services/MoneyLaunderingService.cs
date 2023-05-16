using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.Customer;
using TrustBankConsoleApp.Models;

namespace TrustBankConsoleApp.Services
{
    internal class MoneyLaunderingService : IMoneyLaunderingService
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public MoneyLaunderingService(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }

        public List<CustomerConsoleViewModel> GetAllCustomersByCountry(string countryName, DateTime checkedUpto)
        {
            var customersQuery = _customerService.GetCustomersByCountry(countryName);
            var customerConsoleVMList = new List<CustomerConsoleViewModel>();

            foreach (var customer in customersQuery)
            {
                var customerVM = new CustomerConsoleViewModel();

                customerVM.CustomerId = customer.CustomerId;
                customerVM.Country = customer.Country;
                customerVM.Accounts = _accountService.GetCustomerAccounts(customer.CustomerId).ToList();

                foreach (var account in customerVM.Accounts)
                {
                    customerVM.Transactions = _accountService.GetAllTransactionsByAccountId(account.AccountId)
                        .Where(x => x.Date > checkedUpto).ToList();
                }

                customerConsoleVMList.Add(customerVM);
            }

            return customerConsoleVMList;
        }
        public List<LaunderingRecord> GetSingleMoneyLaunderingRecords(List<CustomerConsoleViewModel> forCustomers)
        {
            var launderingRecords = new List<LaunderingRecord>();

            foreach (var customer in forCustomers)
            {
                foreach (var transaction in customer.Transactions)
                {
                    if (transaction.Amount > 15000)
                    {
                        launderingRecords.Add(new LaunderingRecord
                        {
                            CustomerId = customer.CustomerId,
                            AccountId = transaction.AccountId,
                            TransactionsId = transaction.TransactionId,
                            TransactionDate = transaction.Date,
                            Amount = transaction.Amount
                        });
                    }
                }
            }

            return launderingRecords;
        }
        public List<LaunderingRecord> Get72hMoneyLaunderingRecords(List<CustomerConsoleViewModel> forCustomers)
        {
            var launderingRecords = new List<LaunderingRecord>();

            foreach (var customer in forCustomers)
            {
                foreach (var transaction in customer.Transactions)
                {
                    var transactions72h = customer.Transactions.Where(x => x.Date >= transaction.Date.AddHours(-72));
                    if (transactions72h.Sum(x => x.Amount) > 23000)
                    {

                        launderingRecords.Add(new LaunderingRecord
                        {
                            CustomerId = customer.CustomerId,
                            AccountId = transaction.AccountId,
                            TransactionsId = transaction.TransactionId,
                            TransactionDate = transaction.Date,
                            Amount = transaction.Amount
                        });
                    }
                }
            }

            return launderingRecords;
        }
        public void GenerateMoneyLaunderinReport(List<LaunderingRecord> forMoneyLaunderRecords, string forCountry, string reportType)
        {
            var serial = 0;
            var path = @$"../../../Laundering Reports/{reportType}";
            using (var file = File.CreateText($"{Directory.CreateDirectory(path)}/Report-{reportType}-{forCountry}-{DateTime.Now.ToShortDateString()}.txt"))
            {
                file.WriteLine($"***REPORT TYPE: {reportType} Transactions***");
                file.WriteLine($"***Records for {forCountry}***");
                file.WriteLine($"***Generated on: {DateTime.Now}***");
                file.WriteLine($"***Total records: {forMoneyLaunderRecords.Count()}***");
                file.WriteLine("=========================================================" + Environment.NewLine);

                foreach (var record in forMoneyLaunderRecords)
                {
                    serial += 1;

                    file.WriteLine($"Serial: {serial}");
                    file.WriteLine($"Customer Id: {record.CustomerId}");
                    file.WriteLine($"Account Id: {record.AccountId}");
                    file.WriteLine($"Transaction Id: {record.TransactionsId}");
                    file.WriteLine($"Transaction Date: {record.TransactionDate.ToShortDateString()}");
                    file.WriteLine($"Transaction Amount: {record.Amount}");
                    file.WriteLine("------------------------------------");
                }
            }
        }
    }
}