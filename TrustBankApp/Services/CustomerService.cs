using AutoMapper;
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
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CustomerService(TrustBankDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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


            var customerVMList = query.Select(c => new CustomerViewModel
            {
                CustomerId = c.CustomerId,
                NationalId = c.NationalId,
                FullName = c.Givenname + " " + c.Surname,
                Addres = c.Streetaddress,
                City = c.City,
                Country = c.Country,
            });

            return customerVMList.GetPaged(pageNo, 50);
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

            _mapper.Map(newCustomerViewModel, newCustomer);

            var account = _accountService.GetNewAccount();

            var disposition = _accountService.GetNewDisposition(account);

            newCustomer.Dispositions.Add(disposition);

            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
        }
        public void EditCustomer(EditCustomerViewModel editCustomerViewModel) //add country code and telephone code switch
        {
            var customerToEdit = GetCustomerById(editCustomerViewModel.CustomerId);

            switch (editCustomerViewModel.Country)
            {
                case "Sweden":
                    customerToEdit.CountryCode = "SE";
                    customerToEdit.Telephonecountrycode = "46";
                    break;
                case "Finland":
                    customerToEdit.CountryCode = "FI";
                    customerToEdit.Telephonecountrycode = "358";
                    break;
                case "Denmark":
                    customerToEdit.CountryCode = "DK";
                    customerToEdit.Telephonecountrycode = "45";
                    break;
                case "Norway":
                    customerToEdit.CountryCode = "NO";
                    customerToEdit.Telephonecountrycode = "47";
                    break;
            }

            _mapper.Map(editCustomerViewModel, customerToEdit);

            _dbContext.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _dbContext.Customers.First(x => x.CustomerId == customerId);
        }
    }
}
