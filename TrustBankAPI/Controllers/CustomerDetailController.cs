using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankApp.ViewModels.AccountsVM;
using TrustBankApp.ViewModels.Customer;

namespace TrustBankAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CustomerDetailController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly TrustBankDbContext _dbContext;
        private readonly IMapper _mapper;
        public CustomerDetailController(ICustomerService customerService, IAccountService accountService, IMapper mapper, TrustBankDbContext dbContext)
        {
            _customerService = customerService;
            _accountService = accountService;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public CustomerDetailViewModel Customer { get; set; } = new CustomerDetailViewModel();
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();

        ///<summary>
        ///Retrieve Customer Detail by entering specific customer id.
        ///</summary>
        ///<returns>
        ///Full Customer Detail for a specific Customer
        ///</returns>
        ///<remarks>
        ///Endpoint(example): GET/api/customer/id
        /// </remarks>
        /// <response code="200">
        /// Successfully returned customer detail
        /// </response>

        [Authorize (Roles = "Cashier")]
        [HttpGet]
        [Route("customer/{id}")]
        public async Task<ActionResult<CustomerDetailViewModel>> GetCustomerDetail(int id)
        {
            var customerToShow = _customerService.GetCustomerById(id);
            
            if(customerToShow == null)            
                return BadRequest("Customer is not found");

            _mapper.Map(customerToShow, Customer);
            return Ok(Customer);
        }

        ///<summary>
        ///Retrieve Transactions Detail by entering specific Account id.
        ///</summary>
        ///<returns>
        ///Transactions Detail for a specific Account
        ///</returns>
        ///<remarks>
        ///Endpoint(example): GET/api/account/id/{limit}/{offset}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned transactions detail
        /// </response>
        [HttpGet]
        [Route("account/{id}/{limit}/{offset}")]
        [Authorize(Roles = "Cashier")]
        public async Task<ActionResult<List<TransactionViewModel>>> GetAccountTransactions(int id, int limit, int offset)
        {
            var account = _accountService.GetAccountById(id);

            if (account == null)
                return BadRequest("Account not found");
            
            var transactionsList = _accountService.GetAllTransactionsByAccountId(account.AccountId)
                .OrderByDescending(x => x.Date)
                .Take(limit)
                .Skip(offset)
                .ToList();

            _mapper.Map(transactionsList, Transactions);
            return Ok(Transactions);

        }
    }
}
