using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimbaToursEastAfrica.DataAccess;
using SimbaToursEastAfrica.Domain.Models;
using SimbaToursEastAfrica.Models;
using SimbaToursEastAfrica.Services.EmailServices.Concretes;
using SimbaToursEastAfrica.Services.EmailServices.Interfaces;
using SimbaToursEastAfrica.Services.RepositoryServices.Abstracts;
using SimbaToursEastAfrica.Services.RepositoryServices.Concretes;
using SimbaToursEastAfrica.UnitOfWork.Concretes;
using SimbaToursEastAfrica.UnitOfWork.Interfaces;

namespace SimbaToursEastAfrica
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });

            var connectionString = Configuration.GetConnectionString("SimbaToursEastAfrica");
            var identityConectionString = Configuration.GetConnectionString("DefaultConnection");

            services.Configure<ApplicationConstants.ApplicationConstants>(Configuration.GetSection("ApplicationConstants"));

            services.AddDbContext<SimbaToursEastAfricaDbContext>(
                options => { options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SimbaToursEastAfrica"));
            });
            services.AddDbContext<IdentityDbContext>(
                options => { options.UseSqlServer(identityConectionString, b => b.MigrationsAssembly("SimbaToursEastAfrica"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;
                /*
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                */
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddTransient<DbContext, SimbaToursEastAfricaDbContext>();
            services.AddTransient<IMailService, EmailService>();
            services.AddTransient<IUnitOfWork, SimbaToursEastAfricaUnitOfWork>();
            services.AddTransient<AbstractRepository<Address>, AddressRepository>();
            services.AddTransient<AbstractRepository<Destination>, DestinationRepository>();
            services.AddTransient<AbstractRepository<Driver>, DriverRepository>();
            services.AddTransient<AbstractRepository<HotelBooking>, HotelBookingRepository>();
            services.AddTransient<AbstractRepository<InAndOutBoundAirTravel>, InAndOutBoundAirTravelRepository>();
            services.AddTransient<AbstractRepository<InternalVehicleTravel>, InternalVehicleTravelRepository>();
            services.AddTransient<AbstractRepository<Invoice>, InvoiceRepository>();
            services.AddTransient<AbstractRepository<Item>, ItemRepository>();
            services.AddTransient<AbstractRepository<Itinary>, ItinaryRepository>();
            services.AddTransient<AbstractRepository<Laguage>, LaguageRepository>();
            services.AddTransient<AbstractRepository<Location>, LocationRepository>();
            services.AddTransient<AbstractRepository<Meal>, MealRepository>();
            services.AddTransient<AbstractRepository<Schedule>, ScheduleRepository>();
            services.AddTransient<AbstractRepository<TourClient>, TourClientRepository>(); 
            services.AddTransient<AbstractRepository<Vehicle>, VehicleRepository>();
            services.AddTransient<AbstractRepository<SchedulesPricing>, SchedulesPricingRepository>();
            services.AddTransient<AbstractRepository<MealPricing>, MealsPricingRepository>();
            services.AddTransient<AbstractRepository<LaguagePricing>, LaguagePricingRepository>(); 
            services.AddTransient<AbstractRepository<DealsPricing>, DealsPricingRepository>();
            services.AddTransient<AbstractRepository<HotelPricing>, HotelPricingRepository>();
            services.AddTransient<AbstractRepository<Hotel>, HotelRepository>();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                var tourClientReverseMap = cfg.CreateMap<TourClientViewModel, TourClient>();
                tourClientReverseMap.ReverseMap();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               /*app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                   // HotModuleReplacement = true
                });*/
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
