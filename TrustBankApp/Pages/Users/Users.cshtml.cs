using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.User;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrustBankApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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

            var query = _userService.GetAllUsers(sortColumn, sortOrder, pageNo, searchText);

            var userResult = query.Results.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Role = _userService.GetUserRole(x.Id)
            }).ToList();
                        
            Users = userResult;
        }
    }
}
