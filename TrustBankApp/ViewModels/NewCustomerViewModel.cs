namespace TrustBankApp.ViewModels
{
    public class NewCustomerViewModel
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }
        public string NationalId { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
    }
}
