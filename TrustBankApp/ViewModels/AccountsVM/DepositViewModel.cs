using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrustBankApp.Infrastructure.Validation;

namespace TrustBankApp.ViewModels.AccountsVM
{
    public class DepositViewModel
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }

        [Required]
        [PositiveNumber]
        [Range(10, 100000, ErrorMessage = "The amount should be between 10 to 100000")]
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

    }
}
