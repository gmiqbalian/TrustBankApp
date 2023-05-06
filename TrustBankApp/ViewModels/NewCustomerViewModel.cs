using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TrustBankApp.Infrastructure.Validation;

namespace TrustBankApp.ViewModels
{
    public class NewCustomerViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(100)]
        [RegularExpression("^(?:[a-z]|[A-Z])+$", ErrorMessage = "Can not enter number in name field.")]
        public string GivenName { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^(?:[a-z]|[A-Z])+$", ErrorMessage = "Can not enter number in name field.")]
        [MaxLength(100)]
        public string SurName { get; set; } = null!;
        
        [MaxLength(20)]
        public string? NationalId { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public string Gender { get; set; } = null!;
        
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        
        [MaxLength(10)]
        public string? TelephoneCountryCode { get; set; }
        
        [MaxLength(25)]
        public string? TelephoneNumber { get; set; }
        
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? EmailAddress { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; } = null!;
        
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = null!;
        
        [Required]
        [MaxLength(15)]
        [RegularExpression("^[0-9]{3,5}$", ErrorMessage = "Not a valid zip")]
        public string ZipCode { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = null!;
        public List<SelectListItem>? GendersDropDownList { get; set; }
        public List<SelectListItem>? CountriesDropDownList { get; set; }
    }
}
