using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.User;

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

        public UpdateUserModel(IUserService userService, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UpdateUserModel> logger)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }
        public string Username { get; set; }
        public string StatusMessage { get; set; }
        public EditUserViewModel EditUserVM { get; set; } = new EditUserViewModel();
        private async Task LoadAsync(string userId)
        {
            EditUserVM.Id = userId;

            var user = _userService.GetUserById(userId);
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            EditUserVM.PhoneNumber = phoneNumber;
        }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var user = _userService.GetUserById(userId);
            //var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
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

            await _signInManager.RefreshSignInAsync(singedInUser);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage("/Users/Users");
        }
    }
}
