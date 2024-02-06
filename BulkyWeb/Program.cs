using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using BulkyBook.DataAccess.DBInitializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// ConfigureApplicationCookie used for authorization 
// ConfigureApplicationCookie always add after AddIdentity
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

});

// Register by facebook account :
// First Go to https://developers.facebook.com/docs/facebook-login/web
// and click on Facebook Developer Account and login to facebook account
// and Create App => Other Select Next=> Select an app type : Consumer next =>
// => Add an app name and App contact email => enter your facebook account password
// => Select facebook login setup => Select Web (www) => Site Url in Site Url copy your localhost url
// => https://localhost:7289/ => save changes => continue => next => next => next 
// Then Go to left side tab called Facebook Login => Settings => Go to Valid OAuth Redirect URIs
// and copy https://localhost:7289/sign-facebook this url in Valid OAuth.
// Then Go to left side tab App Settings => Basic => Copy App ID and App secret to use in below code
// Then install NuGet Package for facebook authentication 
// Package name : Microsoft.Aspnetcore.Authentication.Facebook install this package in BulkyBookWeb.

builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "1837703450077539";
    option.AppSecret = "98bd72b18606367ab64a66b4ea009bef";
});




builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});




builder.Services.AddScoped<IDBInitializer,DBInitializer>();

// for working register and login we need the below code
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();

// For authenticate user always write before UseAuthorization() line
app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

SeedDatabase();

// Mapping razor pages for register and login
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using(var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
        dbInitializer.Initialize();
    }
}