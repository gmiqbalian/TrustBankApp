using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Services;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;

        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string SearchText { get; set; }
        public int CurrentPageNumber { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TotalPages { get; set; }

        public List<UserViewModel> Users { get; set; }
        public void OnGet(string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            if (pageNo <= 0)
                pageNo = 1;

            var userResult = _userService.GetAllUsers(sortColumn, sortOrder, pageNo, searchText);
            
            Users = userResult.Results.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Role = _userService.GetUserRole(x.Id)
            }).ToList();
        }
    }
}
