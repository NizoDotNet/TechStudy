using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Repositories;
using TechStudy.RazorPages.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddRazorPages();
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

app.Run();
