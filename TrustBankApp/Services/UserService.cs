using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Services
{
    public class UserService : IUserService
    {
        private readonly TrustBankDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(TrustBankDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
    }
}
