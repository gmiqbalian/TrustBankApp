using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using TrustBankApp.Services;

namespace TrustBankAPI.User
{
    public class UserCredentials
    {
        private readonly TrustBankDbContext _dbContext;
        public UserCredentials(TrustBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserModel> GetUsers()
        {
            var usersQuery = _dbContext.AspNetUsers.Include(x => x.Roles).AsQueryable();
            var users = usersQuery
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    PasswordHash = x.PasswordHash,
                    Role = x.Roles.Select(x => x.Name).ToArray().First().ToString()
                }).ToList();

            return users;
        }
    }
}
