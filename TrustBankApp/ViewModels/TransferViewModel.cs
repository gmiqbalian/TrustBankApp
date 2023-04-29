using System.ComponentModel.DataAnnotations;
using TrustBankApp.Infrastructure.Validation;

namespace TrustBankApp.ViewModels
{
    public class TransferViewModel
    {
        public int ToAccountId { get; set; }
        public int FromAccountId { get; set; }
        public decimal FromAccountBalance { get; set; }
        [Range(10, 100000, ErrorMessage = "You can only transfer an amount between 1 to 100000 kr")]
        [PositiveNumber]
        public decimal Amount { get; set; }
    }
}
