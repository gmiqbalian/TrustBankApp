using Microsoft.AspNetCore.Identity;
using TrustBankApp.Models;

namespace TrustBankAPI.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
