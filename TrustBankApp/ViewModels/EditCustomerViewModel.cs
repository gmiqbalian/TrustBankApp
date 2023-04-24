using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TrustBankApp.ViewModels
{
    public class EditCustomerViewModel
    {
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string GivenName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string SurName { get; set; } = null!;
        [MaxLength(20)]
        public string? NationalId { get; set; }
        [Required]
        public string Gender { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }        
        [MaxLength(25)]
        public string? TelephoneNumber { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(15)]
        public string ZipCode { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = null!;
        public List<SelectListItem>? GendersDropDownList { get; set; }
        public List<SelectListItem>? CountriesDropDownList { get; set; }

    }
}
