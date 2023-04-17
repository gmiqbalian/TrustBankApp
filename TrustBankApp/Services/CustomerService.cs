using Microsoft.Data.SqlClient;
using System.Linq;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.Pages;
using static TrustBankApp.Pages.CustomersModel;

namespace TrustBankApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly TrustBankDbContext _dbContext;

        public CustomerService(TrustBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<CustomerViewModel> GetCustomers(string sortColumn, string sortOrder, int pageNo, string searchText)
        {
            var query = _dbContext.Customers.Select(c => new CustomerViewModel
            {
                CustomerId = c.CustomerId,
                NationalId = c.NationalId,
                FirstName = c.Givenname,
                LastName = c.Surname,
                Addres = c.Streetaddress,
                City = c.City,
                Country = c.Country,
            });

            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    query = query.Where(q => q.CustomerId.ToString() == searchText || 
            //        q.Givenname.ToLower().Contains(searchText) || 
            //        q.Surname.ToLower().Contains(searchText) ||
            //        q.Streetaddress.ToLower().Contains(searchText) ||
            //        q.City.ToLower().Contains(searchText));
            //}


            if (sortColumn == "customerId")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.CustomerId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.CustomerId);

            if (sortColumn == "nationalId")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.NationalId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.NationalId);

            if (sortColumn == "firstName")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.FirstName);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.FirstName);

            if (sortColumn == "lastName")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.LastName);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.LastName);

            if (sortColumn == "address")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Addres);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Addres);

            if (sortColumn == "city")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.City);

            if (sortColumn == "country")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Country);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Country);

            //var itemIndex = (pageNo - 1) * 30;
            //query = query.Skip(itemIndex).Take(30);

            return query.GetPaged(pageNo, 30);
        }
    }
}
