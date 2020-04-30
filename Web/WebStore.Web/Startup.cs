namespace WebStore.Web
{
    using System.Reflection;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WebStore.Data;
    using WebStore.Data.Common;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Data.Repositories;
    using WebStore.Data.Seeding;
    using WebStore.Services;
    using WebStore.Services.Data;
    using WebStore.Services.Mapping;
    using WebStore.Services.Messaging;
    using WebStore.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    });
            services.AddRazorPages();

            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                    {
                        facebookOptions.AppId = this.configuration["Authentication:Facebook:AppId"];
                        facebookOptions.AppSecret = this.configuration["Authentication:Facebook:AppSecret"];

                        facebookOptions.ClaimActions.MapJsonKey("urn:facebook:first_name", "first_name", "string");
                        facebookOptions.ClaimActions.MapJsonKey("urn:facebook:last_name", "last_name", "string");
                        facebookOptions.ClaimActions.MapJsonKey("urn:facebook:gender", "gender", "string");
                        facebookOptions.ClaimActions.MapJsonKey("urn:facebook:birthday", "birthday", "string");
                        facebookOptions.ClaimActions.MapJsonKey("urn:facebook:profile_pic", "profile_pic", "string");
                    });

            services.AddSingleton(this.configuration);

            Account account = new Account(
                              this.configuration["Cloudinary:MyCloudName"],
                              this.configuration["Cloudinary:MyApiKey"],
                              this.configuration["Cloudinary:MyApiKeySecret"]);

            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            var sendGridApiKey = this.configuration["SendGrid:MyApiKey"];
            services.AddScoped<IEmailSender>(x => new SendGridEmailSender(sendGridApiKey));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IRequestsToUsService, RequestsToUsService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IShoppingCartItemsService, ShoppingCartItemsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAddressesService, AddressesService>();
            services.AddTransient<IFavoritesService, FavoritesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
