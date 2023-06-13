using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Seeding;

public static class AppDataInit
{
    private static readonly Guid AdminId = Guid.Parse("d327cf6a-c071-48b2-8f77-cf28fab2496e");

    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        var roleNames = new[] {"Admin", "BasicUser"};
        foreach (var roleName in roleNames)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;

            if (role != null) continue;
            role = new AppRole {Name = roleName};
            var langResult = roleManager.CreateAsync(role).Result;
            if (!langResult.Succeeded)
            {
                throw new ApplicationException("Role creation failed!");
            }
        }

        (Guid id, string email, string pwd) userData = (AdminId, "admin@admin.com", "Vaarikas#1");

        var user = userManager.FindByNameAsync(userData.email).Result;

        if (user != null) return;

        user = new AppUser
        {
            Id = userData.id,
            Email = userData.email,
            UserName = userData.email,
            FirstName = "admin",
            LastName = "boss",
            EmailConfirmed = true
        };

        var userResult = userManager.CreateAsync(user, userData.pwd).Result;
        if (!userResult.Succeeded)
        {
            throw new ApplicationException("User creation failed!");
        }

        var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
        if (!roleResult.Succeeded)
        {
            throw new ApplicationException("Role creation failed!");
        }
    }

    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedIngredientsData(context);

        context.SaveChanges();
    }

    private static void SeedIngredientsData(ApplicationDbContext context)
    {
        if (context.Ingredients.Any()) return;
        context.Ingredients.Add(new Ingredient {Name = "Milk"});
        context.Ingredients.Add(new Ingredient {Name = "Flour"});
        context.Ingredients.Add(new Ingredient {Name = "Egg"});
        context.Ingredients.Add(new Ingredient {Name = "Sugar"});
        context.Ingredients.Add(new Ingredient {Name = "Butter"});
        context.Ingredients.Add(new Ingredient {Name = "Olive oil"});
        context.Ingredients.Add(new Ingredient {Name = "Salt"});
        context.Ingredients.Add(new Ingredient {Name = "Onion"});
        context.Ingredients.Add(new Ingredient {Name = "Tomato paste"});
        context.Ingredients.Add(new Ingredient {Name = "Tomato paste"});
        context.Ingredients.Add(new Ingredient {Name = "Garlic"});
        context.Ingredients.Add(new Ingredient {Name = "Avocado"});
        context.Ingredients.Add(new Ingredient {Name = "Pepper"});
        context.Ingredients.Add(new Ingredient {Name = "Greek yogurt"});
    }
    
   

    
}
