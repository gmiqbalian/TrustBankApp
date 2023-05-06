using TrustBankApp.Models;

namespace TrustBankApp.ViewModels
{
    public class CustomerDetailViewModel
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int AccountId { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
