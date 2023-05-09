using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace TrustBankApp.Pages.Users
{
    [Authorize (Roles = "Admin")]
    [BindProperties]
    public class UpdateUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UpdateUserModel> _logger;
        private readonly IMapper _mapper;

        public UpdateUserModel(IUserService userService, 
            IMapper mapper, 
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            ILogger<UpdateUserModel> logger, 
            IEmailSender emailSender)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string StatusMessage { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public EditUserViewModel EditUserVM { get; set; } = new EditUserViewModel();
        private async Task LoadAsync(string userId)
        {

            var user = _userService.GetUserById(userId);
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var userRole = await _userManager.GetRolesAsync(user);

            EditUserVM.Id = userId;
            EditUserVM.PhoneNumber = phoneNumber;
            EditUserVM.CurrentEmail = email;
            EditUserVM.UserName = userName;
            EditUserVM.UserRole = userRole.First();
        }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            await LoadAsync(userId);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string userId)
        {

            var singedInUser = await _userManager.GetUserAsync(User);

            var userToChange = _userService.GetUserById(userId);
            if (userToChange == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //Change Phone Number
            var phoneNumber = await _userManager.GetPhoneNumberAsync(userToChange);
            if (EditUserVM.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(userToChange, EditUserVM.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            //Change Password
            if(EditUserVM.NewPassword != null)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(userToChange, EditUserVM.OldPassword, EditUserVM.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            //Change User Role
            var currentRole = _userManager.GetRolesAsync(userToChange).Result.First();
            if(EditUserVM.UserRole != currentRole)
            {
                await _userManager.RemoveFromRoleAsync(userToChange, currentRole);
                await _userManager.AddToRoleAsync(userToChange, EditUserVM.UserRole);
            }

            await _signInManager.RefreshSignInAsync(singedInUser);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your profile has been updated";
            return RedirectToPage("/Users/Users");
        }
    }
}
