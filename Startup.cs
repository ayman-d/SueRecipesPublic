using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SueRecipes.Data;
using SueRecipes.Models.ApplicationUserModel;
using SueRecipes.Models.RecipeModel;
using SueRecipes.Models.IngredientModel;
using SueRecipes.Models.CommentModel;
using SueRecipes.Models.HeartModel;

namespace SueRecipes
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
            services.AddControllersWithViews();
            // Add dependancy injection for all models and repositories
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IHeartRepository, HeartRepository>();
            // Add Db Context dependancy injection and DbConnection
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SueRecipesDBConnection")));
            // Add Identity and Roles
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            // Add specifications for password complexity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
