using Microsoft.AspNetCore.Identity;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.ViewModels.User;

namespace TrustBankApp.Services
{
    public interface IUserService
    {
        PagedResult<IdentityUser> GetAllUsers(string sortColumn, string sortOrder, int pageNo, string searchText);
        string GetUserRole(string userId);
        IdentityUser GetUserById(string userId); 
    }
}
