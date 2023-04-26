namespace TrustBankApp.ViewModels
{
    public class TransferViewModel
    {
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public int FromAccountId { get; set; }
        public decimal FromAccountBalance { get; set; }

    }
}
