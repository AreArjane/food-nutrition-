using Microsoft.EntityFrameworkCore;
using FoodRegistrationToolSub1.Models.datasets;
using FoodRegisterationToolSub1.Models.permissions;
using FoodRegisterationToolSub1.Models.users;
using FoodRegisterationToolSub1.Models.datasets;
using FoodRegistrationToolSub1.Models;
using FoodRegisterationToolSub1.Models.meals;
using System.ComponentModel.DataAnnotations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodNutrient> FoodNutrients { get; set; }
    public DbSet<Nutrient> Nutrients { get; set; }
    public DbSet<FoodCategory> FoodCategories { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<User> Users {get; set;}
    public DbSet<NormalUser> NormalUsers { get; set; }
    public DbSet<SuperUser> SuperUsers { get; set; }
    public DbSet<AdminUser> AdminUser { get; set; }
    public DbSet<PendingSuperUser> PendingSuperUser {get; set;}
    public DbSet<Meal> Meal {get; set;}
    public DbSet<MealFood> MealFood {get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<NormalUser>().ToTable("NormalUsers");
        modelBuilder.Entity<SuperUser>().ToTable("SuperUsers");
        modelBuilder.Entity<AdminUser>().ToTable("AdminUsers");
        modelBuilder.Entity<PendingSuperUser>().ToTable("PendingSuperUser");
        modelBuilder.Entity<Nutrient>()
        .Property(n => n.Id)
        .ValueGeneratedNever();

        modelBuilder.Entity<Food>()
        .Property(f => f.FoodId)
        .ValueGeneratedNever();

        modelBuilder.Entity<FoodNutrient>()
        .Property(fn => fn.Id)
        .ValueGeneratedNever();
        
        modelBuilder.Entity<Nutrient>()
        .Property(n => n.Id)
        .ValueGeneratedNever();

      modelBuilder.Entity<FoodNutrient>()
        .HasOne(fn => fn.Food)
        .WithMany(f => f.FoodNutrients)
        .HasForeignKey(fn => fn.FdcId)
        .OnDelete(DeleteBehavior.Cascade);  // Or use Restrict

    // Many-to-one relationship: Nutrient -> FoodNutrient
    modelBuilder.Entity<FoodNutrient>()
        .HasOne(fn => fn.Nutrient)
        .WithMany(n => n.FoodNutrients)
        .HasForeignKey(fn => fn.NutrientId)
        .OnDelete(DeleteBehavior.Cascade); 
    }

public User CreateUser(string firstName, string lastName, string email, string phoneNr, UserType userType, string password, string dateOfBirth)
{
    User user;

    // Create user based on UserType
    user = userType switch
    {
        UserType.NormalUser => new NormalUser 
        { 
            FirstName = firstName, 
            LastName = lastName, 
            PhoneNr = phoneNr, 
            Email = email 
        },
        UserType.SuperUser => new SuperUser 
            { 
                FirstName = firstName, 
                PhoneNr = phoneNr, 
                Email = email, 
                DateOfBirth = dateOfBirth 
            } ,
        _ => throw new ArgumentException("Invalid user type.")
    };

    // Set password and add to the appropriate DbSet
    user.SetPassword(password);
    if (user is NormalUser normalUser)
    {
        NormalUsers.Add(normalUser);
    }
     if (user is SuperUser superUser)
    {
        SuperUsers.Add(superUser);
    }

    SaveChanges();

    return user;
}


}
