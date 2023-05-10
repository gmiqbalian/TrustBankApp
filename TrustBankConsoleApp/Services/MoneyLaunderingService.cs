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

        public MoneyLaunderingService(TrustBankDbContext dbContext, ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }
        
        public List<CustomerConsoleViewModel> GetAllCustomersByCountry(string countryName)
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
                    customerVM.Transactions = _accountService.GetAllTransactionsByAccountId(account.AccountId).ToList();
                }

                customerConsoleVMList.Add(customerVM);
            }

            return customerConsoleVMList;
        }
        public List<LaunderingRecord> GetMoneyLaunderingRecords(List<CustomerConsoleViewModel> forCustomers, DateTime lastDate)
        {
            var launderingRecords = new List<LaunderingRecord>();


            //to control that no account is checked doubled (one from owner side and one from diposer side)            
            
            //Account last = new Account();
            
            //var otherlist = new List<CustomerConsoleViewModel>();
            //foreach(var customer in forCustomers)
            //{
            //    var accountslist = customer.Accounts.Select(x => x.AccountId);
                
            //    foreach(var no in accountslist)
            //    {
            //        if(last != null)
            //        {
            //            if(no != last.AccountId)
            //            {
            //                otherlist.Add(customer);
            //            }
            //            last.AccountId = no;
            //        }
            //    }
            //}

            foreach(var customer in forCustomers)
            {
                foreach(var transaction in customer.Transactions)
                {
                    if(transaction.Date > lastDate && transaction.Amount > 74000)
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

                    var transactions72h = customer.Transactions.Where(x => x.Date <= lastDate.AddHours(-72));
                    if (transactions72h.Sum(x => x.Amount) > 74000)
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
        public void GenerateMoneyLaunderinReport(List<LaunderingRecord> forMoneyLaunderRecords, string forCountry)
        {
            var serial = 0;
            using (var file = File.CreateText($"../../../Laundering Reports/{forCountry}-{DateTime.Now.ToShortDateString()}.txt"))
            {
                file.WriteLine($"***Records for {forCountry}***");
                file.WriteLine($"***Generated on: {DateTime.Now}***");
                file.WriteLine($"***Total records: {forMoneyLaunderRecords.Count()}***");                
                file.WriteLine("=========================================================" + Environment.NewLine);

                foreach(var record in forMoneyLaunderRecords)
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