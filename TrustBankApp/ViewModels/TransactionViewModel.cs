namespace TrustBankApp.ViewModels
{
    public class TransactionViewModel
    {
        public int AccountId { get; set; }
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
    }
}
