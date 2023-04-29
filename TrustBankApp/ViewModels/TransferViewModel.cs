using System.ComponentModel.DataAnnotations;
using TrustBankApp.Infrastructure.Validation;

namespace TrustBankApp.ViewModels
{
    public class TransferViewModel
    {
        public int ToAccountId { get; set; }
        public int FromAccountId { get; set; }
        public decimal FromAccountBalance { get; set; }
        
        [Range(10, 100000, ErrorMessage = "The amount should be between 10 to 100000")]
        [PositiveNumber]
        public decimal Amount { get; set; }
    }
}
