using Microsoft.AspNetCore.Identity;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.Pages.Users;
using TrustBankApp.ViewModels.User;

namespace TrustBankApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UpdateUserModel> _logger;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UpdateUserModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public PagedResult<IdentityUser> GetAllUsers(string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            var query = _userManager.Users.AsQueryable();

            if (sortColumn == "userId")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Id);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(x => x.Id);

            if (sortColumn == "userName")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.UserName);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(x => x.UserName);

            if (sortColumn == "email")
                if (sortOrder == "asc")
                    query = query.OrderBy(x => x.Email);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(x => x.Email);

            return query.GetPaged(pageNo, 10);
            
        }
        public string GetUserRole(string userId)
        {
            var user = _userManager.Users.First(x => x.Id == userId);
            return _userManager.GetRolesAsync(user)
                .GetAwaiter()
                .GetResult()
                .First()
                .ToString();
        }
        public IdentityUser GetUserById(string userId)
        {
            return _userManager.Users.First(x => x.Id == userId);
        }
    }
}
