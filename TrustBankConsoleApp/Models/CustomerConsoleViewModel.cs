using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBankApp.Models;

namespace TrustBankConsoleApp.Models
{
    internal class CustomerConsoleViewModel
    {
        public int CustomerId { get; set; }
        public string Country { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
