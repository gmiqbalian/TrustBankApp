using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrustBankApp.Infrastructure.Validation;

namespace TrustBankApp.ViewModels.AccountsVM
{
    public class TransferViewModel
    {
        public int CustomerId { get; set; }
        public int FromAccountId { get; set; }
        public decimal FromAccountBalance { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ToAccountId { get; set; }

        [Range(10, 100000, ErrorMessage = "The amount should be between 10 to 100000")]
        [PositiveNumber]
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Amount { get; set; }
    }
}
