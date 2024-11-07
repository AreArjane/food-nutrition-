using Microsoft.EntityFrameworkCore;
using FoodRegistrationToolSub1.Models.datasets;
using FoodRegisterationToolSub1.Models.permissions;
using FoodRegisterationToolSub1.Models.users.User;
using FoodRegisterationToolSub1.Models.datasets;
using FoodRegistrationToolSub1.Models;
using FoodRegisterationToolSub1.Models.meals;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodNutrient> FoodNutrients { get; set; }
    public DbSet<Nutrient> Nutrients { get; set; }
    public DbSet<FoodCategory> FoodCategories { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<NormalUser> NormalUsers { get; set; }
    public DbSet<SuperUser> SuperUsers { get; set; }
    public DbSet<AdminUser> AdminUser { get; set; }
    public DbSet<Meal> Meal {get; set;}
    public DbSet<MealFood> MealFood {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nutrient>()
        .Property(n => n.Id)
        .ValueGeneratedNever();
    }
}
