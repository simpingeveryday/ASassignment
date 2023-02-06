using ASassignment.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebApp_Core_Identity.Model;
using ASassignment;
using Microsoft.AspNetCore.Identity;
using ASassignment.Pages;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
UserManager<ApplicationUser> userManager;
SignInManager<ApplicationUser> signInManager;
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

}).AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<GooglereCaptchaService>();
builder.Services.AddDataProtection();
builder.Services.Configure<ReCAPTCHASettings>(builder.Configuration.GetSection("GooglereCAPTCHA"));

builder.Services.AddScoped<SessionService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache(options =>
{
    options.TrackStatistics = true;
});

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options
=>
{
    options.Cookie.Name = "MyCookieAuth";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Logging.AddDbLogger(options =>
{
    builder.Configuration.GetSection("Logging").GetSection("Database").GetSection("Options").Bind(options);
});


var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.Logger.LogInformation("Adding Routes");
app.Logger.LogInformation("Starting the app");

app.Run();
