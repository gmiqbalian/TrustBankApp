using TrustBankApp.Infrastructure.Pagination;
using static TrustBankApp.Pages.CustomersModel;

namespace TrustBankApp.Services
{
    public interface ICustomerService
    {
        PagedResult<CustomerViewModel> GetCustomers(string sortColumn, string sortOrder, int pageNo, string searchText);
    }
}
