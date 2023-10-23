using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Services.Concretes;
using MiniProcurement.Services.Interfaces;
using System.Globalization;

namespace MiniProcurement.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPurchaseRequestService, PurchaseRequestService>();
            services.AddScoped<IInvoiceRequestService, InvoiceRequestService>();

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
                    new CultureInfo("en-US"),
                    new CultureInfo("zh-Hant")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "zh-Hant", uiCulture: "zh-Hant");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
                //options.RequestCultureProviders.Clear();

            });

            return services;
        }
    }
}
