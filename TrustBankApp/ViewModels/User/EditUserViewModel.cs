using System.ComponentModel.DataAnnotations;

namespace TrustBankApp.ViewModels.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string CurrentEmail { get; set; }

        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        public string NewEmail { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string UserRole { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
