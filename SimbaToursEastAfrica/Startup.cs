using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("LogInCookie",options =>
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
                options.HttpOnly = HttpOnlyPolicy.None;
            });
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

            services.Configure<ApplicationConstants.BusinessEmailDetails>(Configuration.GetSection("BusinessEmailDetails"));
            services.Configure<ApplicationConstants.ApplicationConstants>(Configuration.GetSection("ApplicationConstants"));
            services.Configure<ApplicationConstants.twitterProfileFiguration>(Configuration.GetSection("twitterProfileFiguration"));

            services.AddDbContext<ApplicationDbContext.ApplicationDbContext>(
                options => {
                    options.UseSqlServer(identityConectionString, b => b.MigrationsAssembly("SimbaToursEastAfrica"));
                });
            services.AddDbContext<SimbaToursEastAfricaDbContext>(
                options => { options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SimbaToursEastAfrica.DataAccess"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext.ApplicationDbContext>()
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

            services.AddScoped<DbContext, SimbaToursEastAfricaDbContext>();
            services.AddScoped<IMailService, EmailService>();
            services.AddScoped<IUnitOfWork, SimbaToursEastAfricaUnitOfWork>();
            services.AddScoped<AbstractRepository<Address>, AddressRepository>();
            services.AddScoped<AbstractRepository<Destination>, DestinationRepository>();
            services.AddScoped<AbstractRepository<Driver>, DriverRepository>();
            services.AddScoped<AbstractRepository<HotelBooking>, HotelBookingRepository>();
            services.AddScoped<AbstractRepository<InAndOutBoundAirTravel>, InAndOutBoundAirTravelRepository>();
            services.AddScoped<AbstractRepository<InternalVehicleTravel>, InternalVehicleTravelRepository>();
            services.AddScoped<AbstractRepository<Invoice>, InvoiceRepository>();
            services.AddScoped<AbstractRepository<Item>, ItemRepository>();
            services.AddScoped<AbstractRepository<Itinary>, ItinaryRepository>();
            services.AddScoped<AbstractRepository<Laguage>, LaguageRepository>();
            services.AddScoped<AbstractRepository<Location>, LocationRepository>();
            services.AddScoped<AbstractRepository<Meal>, MealRepository>();
            services.AddScoped<AbstractRepository<Schedule>, ScheduleRepository>();
            services.AddScoped<AbstractRepository<TourClient>, TourClientRepository>(); 
            services.AddScoped<AbstractRepository<Vehicle>, VehicleRepository>();
            services.AddScoped<AbstractRepository<SchedulesPricing>, SchedulesPricingRepository>();
            services.AddScoped<AbstractRepository<MealPricing>, MealsPricingRepository>();
            services.AddScoped<AbstractRepository<LaguagePricing>, LaguagePricingRepository>(); 
            services.AddScoped<AbstractRepository<DealsPricing>, DealsPricingRepository>();
            services.AddScoped<AbstractRepository<DealsPricing>, DealsPricingRepository>();
            services.AddScoped<AbstractRepository<HotelPricing>, HotelPricingRepository>();
            services.AddScoped<AbstractRepository<Hotel>, HotelRepository>(); 
            services.AddScoped<AbstractRepository<TransportPricing>, TransportPricingRepository>();
            services.AddScoped<AbstractRepository<TourClientExtraCharge>, TourClientExtraChargesRepository>();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                var tourClientReverseMap = cfg.CreateMap<TourClientViewModel, TourClient>();
                tourClientReverseMap.ReverseMap();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors("CorsPolicy");

            app.UsePathBase("/SimbaSafariToursV2");

            app.Use((context, next) =>
            {
                context.Request.PathBase = "/SimbaSafariToursV2";
                return next();
            });
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
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
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
