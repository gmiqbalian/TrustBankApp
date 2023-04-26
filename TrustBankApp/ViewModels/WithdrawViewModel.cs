namespace TrustBankApp.ViewModels
{
    public class WithdrawViewModel
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

    }
}
