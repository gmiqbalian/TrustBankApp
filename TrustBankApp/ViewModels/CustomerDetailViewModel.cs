using TrustBankApp.Models;

namespace TrustBankApp.ViewModels
{
    public class CustomerDetailViewModel
    {
        public int CustomerId { get; set; }
        public string NationalId { get; set; }
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string TelephoneCountryCode { get; set; }
        public string TelephoneNumber { get; set; }
        public List<Account> Accounts { get; set; }
        public decimal TotalBalance { get; set; }
        public int Age { get; set; }
    }
}
