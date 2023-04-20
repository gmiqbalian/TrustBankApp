namespace TrustBankApp.ViewModels
{
    public class TopTenAccountsViewModel
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
