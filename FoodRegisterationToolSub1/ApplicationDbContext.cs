using Microsoft.EntityFrameworkCore;
using FoodRegistrationToolSub1.Models.datasets;
using FoodRegisterationToolSub1.Models.permissions;
using FoodRegisterationToolSub1.Models.users.User;
using FoodRegisterationToolSub1.Models.datasets;

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
}
