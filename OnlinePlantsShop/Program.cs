using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OnlinePlantsShop.Data;
using OnlinePlantsShop.Models;
using OnlinePlantsShop_.Data;
using OnlinePlantsShop_.Repositories;
using System.Configuration;
using OnlinePlantsShop.Repository;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<appuser>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<PlantsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlantsDb")));

builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDb")));



builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddScoped<IPlant, PlantRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddSignalR();


builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BusinessHoursOnly", policy =>
        policy.RequireAssertion(context =>
            DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 23));

    options.AddPolicy("PlantAccess", policy =>
        policy.RequireClaim("PlantAccess"));

    options.AddPolicy("CanEditPlants", policy =>
       policy.RequireClaim("PlantAccess", "CanEdit"));

    options.AddPolicy("IndoorPlantManager", policy =>
        policy.RequireClaim("PlantType", "Indoor"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<appuser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedRolesAndAdminAsync(userManager, roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<OnlinePlantsShop_.Hubs.PlantsHub>("/planthub");

app.Run();

async Task SeedRolesAndAdminAsync(UserManager<appuser> userManager, RoleManager<IdentityRole> roleManager)
{
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    if (!await roleManager.RoleExistsAsync("Customer"))
        await roleManager.CreateAsync(new IdentityRole("Customer"));

    var adminEmail = "admin@plantshop.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new appuser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            City = "lahore",
            Country = "Pakistan"
        };

        var createUserResult = await userManager.CreateAsync(adminUser, "Admin@123");
        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            await userManager.AddClaimAsync(adminUser, new Claim("PlantAccess", "CanEdit"));
            
        }
        else
        {
            throw new Exception("Failed to create admin user: " + string.Join(", ", createUserResult.Errors));
        }
    }
    else
    {
        var claims = await userManager.GetClaimsAsync(adminUser);
        if (!claims.Any(c => c.Type == "PlantAccess" && c.Value == "CanEdit"))
            await userManager.AddClaimAsync(adminUser, new Claim("PlantAccess", "CanEdit"));
        
    }
}