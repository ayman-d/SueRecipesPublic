using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SueRecipes.Models.ApplicationUserModel;
using SueRecipes.Models.CommentModel;
using SueRecipes.Models.IngredientModel;
using SueRecipes.Models.HeartModel;
using SueRecipes.Models.RecipeModel;


namespace SueRecipes.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Heart> Heart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Heart>()
                .HasKey(h => new { h.ApplicationUserId, h.RecipeId });
        }
    }
}
