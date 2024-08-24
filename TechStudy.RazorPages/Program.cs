using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using TechStudy.RazorPages.AuthorizationRequirements;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Helpers;
using TechStudy.RazorPages.Repositories;
using TechStudy.RazorPages.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json");

string connectionString = string.Empty;

if(builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("Local") ?? throw new Exception("No connection string");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("Production") ?? throw new Exception("No connection string");
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySQL(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policyBuilder => policyBuilder.AddRequirements(new AdminRoleRequirement()));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton(new ApplicationIdentityClaims());
builder.Services.AddRazorPages(op =>
{
    op.Conventions.AuthorizeFolder("/AccountsManager", "AdminPolicy");
});
builder.Services.Configure<IdentityOptions>(o =>
{
    o.Password.RequiredUniqueChars = 2;
    o.Password.RequireUppercase = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireDigit = false;
    o.User.RequireUniqueEmail = true;

    o.SignIn.RequireConfirmedPhoneNumber = false;
    o.SignIn.RequireConfirmedEmail = false;
    o.SignIn.RequireConfirmedAccount = false;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Use(async (context, next) =>
{

    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var user = await userManager.FindByEmailAsync("mmmdov.sadiq@gmail.com");
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        await userManager.ConfirmEmailAsync(user, code);
    }

    await next.Invoke();
});
app.MapDelete("/claims", async ([FromQuery] string userId, [FromQuery] string type, [FromQuery] string value, IUserService userService) =>
{
    var res = await userService.RemoveClaimAsync(userId, new(type, value));
    if (res)
    {
        return Results.Ok();
    }
    return Results.Problem();
});


app.Run();

