namespace TrustBankApp.ViewModels.AccountsVM
{
    public class AccountDetailViewModel
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Balance { get; set; }
    }
}
