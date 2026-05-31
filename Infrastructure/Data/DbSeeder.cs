using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, IPasswordHasher passwordHasher, string adminEmail, string adminPassword)
    {
        if (!await db.Users.AnyAsync(u => u.Role == "Admin"))
        {
            var hash = passwordHasher.Hash(adminPassword);
            db.Users.Add(new User
            {
                Email = adminEmail,
                PasswordHash = hash,
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }
    }
}
