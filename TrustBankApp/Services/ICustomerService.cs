using Microsoft.AspNetCore.Mvc.Rendering;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Services
{
    public interface ICustomerService
    {
        PagedResult<CustomerViewModel> GetCustomers(string sortColumn, string sortOrder, int pageNo, string searchText);
        void CreateNewCustomer(NewCustomerViewModel newCustomerViewModel);
        void EditCustomer(EditCustomerViewModel editCustomerViewModel);
        List<SelectListItem> FillGenderDropDownList();
        List<SelectListItem> FillCountryDropDownList();
        Customer GetCustomerById(int customerId);
    }
}
