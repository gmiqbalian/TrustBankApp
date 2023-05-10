using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TrustBankApp.Models;
using TrustBankApp.Services;
using TrustBankConsoleApp;
using TrustBankConsoleApp.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
        services.AddDbContext<TrustBankDbContext>(options =>
        options.UseSqlServer(
        context.Configuration.GetConnectionString("DefaultConnection")))
            .AddTransient<ICustomerService, CustomerService>()
            .AddTransient<IAccountService, AccountService>()
            .AddTransient<IMoneyLaunderingService, MoneyLaunderingService>()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddTransient<Application>())
    .Build();




using (var scope = host.Services.CreateScope())
{
    scope.ServiceProvider.GetService<Application>().Run();
}

host.Run();