using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Services.Concretes;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => { c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });

        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IPurchaseRequestService, PurchaseRequestService>();
        services.AddScoped<IInvoiceRequestService, InvoiceRequestService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IConveyorService, ConveyorService>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new("en-US"),
                new("zh-Hant")
            };

            options.DefaultRequestCulture = new RequestCulture("zh-Hant", "zh-Hant");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new AcceptLanguageHeaderRequestCultureProvider()
            };
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "localhost:telman",
                    ValidAudience = "MiniProcurementApp"
                };
            });

        services.AddHttpContextAccessor();

        return services;
    }
}