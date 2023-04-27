using Microsoft.AspNetCore.Mvc.Rendering;
using TrustBankApp.Infrastructure.Pagination;
using TrustBankApp.Models;
using TrustBankApp.Models.DropDowns;
using TrustBankApp.ViewModels;

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
            var query = _dbContext.Customers.AsQueryable();
            
            if(string.IsNullOrEmpty(searchText))
            {
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

                if (sortColumn == "name")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Givenname)
                            .ThenBy(c => c.Surname);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Givenname)
                            .ThenByDescending(c => c.Surname);

                if (sortColumn == "address")
                    if (sortOrder == "asc")
                        query = query.OrderBy(c => c.Streetaddress);
                    else if (sortOrder == "desc")
                        query = query.OrderByDescending(c => c.Streetaddress);

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
            }
            else if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(q => q.CustomerId.ToString() == searchText ||
                    q.Givenname.ToLower().Contains(searchText) ||
                    q.Surname.ToLower().Contains(searchText) ||
                    q.Streetaddress.ToLower().Contains(searchText) ||
                    q.City.ToLower().Contains(searchText));
            }

            var customerViewModelList = query.Select(c => new CustomerViewModel
                {
                    CustomerId = c.CustomerId,
                    NationalId = c.NationalId,
                    FullName = c.Givenname + " " + c.Surname,
                    Addres = c.Streetaddress,
                    City = c.City,
                    Country = c.Country,
                });

            
            //var itemIndex = (pageNo - 1) * 30;
            //query = query.Skip(itemIndex).Take(30);
            
            return customerViewModelList.GetPaged(pageNo, 30);
        }

        public List<SelectListItem> FillGenderDropDownList()
        {
            return Enum.GetValues<Gender>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString(),
                    Value = x.ToString()
                }).ToList();
        }
        public List<SelectListItem> FillCountryDropDownList()
        {
            return Enum.GetValues<Country>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString(),
                    Value = x.ToString()
                }).ToList();
        }

        public void CreateNewCustomer(NewCustomerViewModel newCustomerViewModel)
        {
            var newCustomer = new Customer();

            newCustomer.NationalId = newCustomerViewModel.NationalId;
            newCustomer.Gender = newCustomerViewModel.Gender.ToLower();
            newCustomer.Telephonenumber = newCustomerViewModel.TelephoneNumber;
            newCustomer.Birthday = Convert.ToDateTime(newCustomerViewModel.Birthday);
            newCustomer.Givenname = newCustomerViewModel.GivenName;
            newCustomer.Surname = newCustomerViewModel.SurName;
            newCustomer.City = newCustomerViewModel.City;
            newCustomer.Emailaddress = newCustomerViewModel.Email;
            newCustomer.Country = newCustomerViewModel.Country;
            newCustomer.Streetaddress = newCustomerViewModel.StreetAddress;
            newCustomer.Zipcode = newCustomerViewModel.ZipCode;

            switch (newCustomerViewModel.Country)
            {
                case "Sweden":
                    newCustomer.CountryCode = "SE";
                    newCustomer.Telephonecountrycode = "46";
                    break;
                case "Finland":
                    newCustomer.CountryCode = "FI";
                    newCustomer.Telephonecountrycode = "358";
                    break;
                case "Denmark":
                    newCustomer.CountryCode = "DK";
                    newCustomer.Telephonecountrycode = "45";
                    break;
                case "Norway":
                    newCustomer.CountryCode = "NO";
                    newCustomer.Telephonecountrycode = "47";
                    break;
            }

            var account = new Account();
            account.Created = new DateTime(1990-01-01);
            account.Frequency = "Monthly";

            var disposition = new Disposition();
            disposition.Account = account;
            disposition.Type = "Owner";

            newCustomer.Dispositions.Add(disposition);

            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _dbContext.Customers.First(x => x.CustomerId == customerId);
        }
    }
}
