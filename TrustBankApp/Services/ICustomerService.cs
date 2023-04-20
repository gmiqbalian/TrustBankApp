using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.ViewModels;

namespace TrustBankApp.Services
{
    public interface ICustomerService
    {
        PagedResult<CustomerViewModel> GetCustomers(string sortColumn, string sortOrder, int pageNo, string searchText);
    }
}
