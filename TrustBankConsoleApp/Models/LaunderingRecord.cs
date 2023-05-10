using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustBankConsoleApp.Models
{
    internal class LaunderingRecord
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public int TransactionsId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
    }
}
