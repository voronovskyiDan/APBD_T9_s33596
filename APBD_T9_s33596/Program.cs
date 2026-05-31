using Application.Interfaces;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Common.Security;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

var pepper = builder.Configuration["Security:Pepper"]
    ?? throw new InvalidOperationException("Security:Pepper is not configured");
builder.Services.AddScoped<IPasswordHasher>(_ => new BCryptPasswordHasher(pepper));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<AppDbContext>();
    var config = services.GetRequiredService<IConfiguration>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher>();

    await db.Database.MigrateAsync();

    string adminEmail = config["Seed:AdminEmail"]!;
    string adminPassword = config["Seed:AdminPassword"]!;
    if (string.IsNullOrWhiteSpace(adminEmail) ||
           string.IsNullOrWhiteSpace(adminPassword))
    {
        throw new Exception("Admin seed configuration is missing (Seed:AdminEmail / Seed:AdminPassword).");
    }

    await DbSeeder.SeedAsync(db, passwordHasher, adminEmail, adminPassword);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
