using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TrustBankAPI.User;
using TrustBankApp.Models;
using TrustBankApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sw =>
{
    //General description for Swagger
    sw.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.00",
        Title = "Trust Bank API",
        Description = @"API for retrieving customer and customer account information",
        Contact = new OpenApiContact
        {
            Name = "Ghazanfar Mahmood",
            Email = "gmiqbalian@gmail.com"
        }
    });

    //XML (Description)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sw.IncludeXmlComments(xmlPath);

    //Authorization
    sw.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    //Authorization
    sw.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

//Add DB to the container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TrustBankDbContext>(options => options.UseSqlServer(connectionString));

//Services
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<PasswordHasher<UserModel>>();
builder.Services.AddTransient<UserCredentials>();


//Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//JWT Token authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//builder.Services.AddMvc();
//builder.Services.AddControllers();

//CORS (cross-origin request sharing)
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//CORS
app.UseCors("AllowAll");

//JWT token authorization
app.UseAuthentication();

app.UseAuthorization();

//app.MapControllers();

app.MapControllers();

app.Run();
