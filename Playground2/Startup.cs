using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Playground2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground2.Data.Interfaces;
using Playground2.Data.Repositories;
using Playground2.Data.Models;
using Playground2.Data.Mocks;

namespace Playground2
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

            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IIcecreamRepository, IcecreamRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Cart.GetCart(sp)); //For multiple access of cart, Ehem, what's sp

            services.AddTransient<IOrderRepository, OrderRepository>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache(); //Ehem
            services.AddSession(); //Ehem
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ApplicationDbContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();//Ehem

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Seed(app);

            CreateRolesandUsersAsync(context, userManager, roleManager).Wait();
        }
        public async Task CreateRolesandUsersAsync(ApplicationDbContext context,
        UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string[] roles = { "Boss", "Kitchen", "Bar", "Client" };
            foreach (var role in roles)
            {
                var doesExist = await roleManager.RoleExistsAsync(role);
                if (!doesExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName: role));
                }
            }

            string[] users = { "Boss1@playground.com",
                               "Kitchen1@playground.com",
                               "Bar1@playground.com",
                               "Client1@playground.com" };

            int count = -1;
            foreach (var user in users)
            {
                count++;
                if (await userManager.FindByNameAsync(user) == null)
                {
                    var userObj = new IdentityUser() { UserName = user, Email = user, };
                    var result = await userManager.CreateAsync(userObj);
                    if (result.Succeeded)
                    {
                        await userManager.AddPasswordAsync(userObj, user);
                        await userManager.AddToRoleAsync(userObj, roles[count]);
                    }
                }
            }
        }
    }
}
