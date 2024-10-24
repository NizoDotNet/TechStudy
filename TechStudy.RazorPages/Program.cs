using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using TechStudy.RazorPages.AuthorizationRequirements;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Helpers;
using TechStudy.RazorPages.Helpers.Middlewares;
using TechStudy.RazorPages.Helpers.Options;
using TechStudy.RazorPages.Repositories;
using TechStudy.RazorPages.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json");

string connectionString = string.Empty;

if (builder.Environment.IsDevelopment())
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

builder.Services.AddDefaultIdentity<TechStudyUser>(o => 
{
    o.SignIn.RequireConfirmedAccount = true;
    o.Password.RequiredUniqueChars = 2;
    o.Password.RequireUppercase = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireDigit = false;
    o.User.RequireUniqueEmail = true;

    o.SignIn.RequireConfirmedPhoneNumber = false;
    o.SignIn.RequireConfirmedEmail = false;
    o.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policyBuilder => policyBuilder.AddRequirements(new AdminRoleRequirement()));
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton(new ApplicationIdentityClaims());
builder.Services.AddTransient<ExceptionHandler>();
builder.Services.AddScoped<IGroupRepository,  GroupRepository>();
builder.Services.AddScoped<IGroupService, GroupService>();
//builder.Services.AddScoped<IEmailSender, MailKitEmailSender>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddRazorPages(op =>
{
    op.Conventions.AuthorizeFolder("/AccountsManager", "AdminPolicy");
    op.Conventions.AuthorizeAreaFolder("Identity", "/Account", "AdminPolicy");
    op.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Register");
    op.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Login");
    op.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Logout");
    op.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/AccessDenied");
    
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";

});
builder.Services.Configure<EmailOption>(builder.Configuration.GetSection("EmailOption"));
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

app.UseMiddleware<ExceptionHandler>();

app.MapRazorPages();

app.MapDelete("/claims", async ([FromQuery] string userId,
    [FromQuery] string type,
    [FromQuery] string value,
    IUserService userService,
    HttpContext context) =>
{
    var res = await userService.RemoveClaimAsync(userId, new(type, value));
    if (res)
    {
        app.Logger.LogWarning("User's claim with {type} {value} " +
                    "was deleted by {AdminEmail}. User's {userId}",
                    type, value,
                    context.User.FindFirstValue(ClaimTypes.Email),
                    userId);
        return Results.Ok();
    }
    return Results.Problem();
}).RequireAuthorization("AdminPolicy");


app.Run();

