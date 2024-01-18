using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NToastNotify;
using Project.Data.Context;
using Project.Data.Entities;
using Project.Data.Extensions;
using Project.Service.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//
// Service ekleme islemleri ilgili projenin Extensions klasoru altinda yapilmakta.
builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
builder.Services.AddSession();
//
//

//
// Logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();
//
//

//
// Add services to the container. and Toaster
builder.Services.AddControllersWithViews()
    .AddNToastNotifyToastr(new ToastrOptions()
    {
        PositionClass = ToastPositions.TopRight,
        TimeOut = 3000,
    });
//
//

//
// Identity icin belirli kurallar ayarlaniyor.
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    // test asamasindaki zorlugu engellemek icin bazi ayarlar. daha sonradan degistirilmeli.
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
   
    
})
    .AddRoleManager<RoleManager<AppRole>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
//
//

//
//
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Account/Auth/Login");
    config.LogoutPath = new PathString("/Account/User/Logout");
    config.Cookie = new CookieBuilder
    {
        Name = "appCookie",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest     // canliya ciktiginda .Always olmali
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7);
    config.AccessDeniedPath = new PathString("/Account/Auth/AccessDenied");
});
//
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseStatusCodePagesWithRedirects("/Account/Auth/Error/{0}");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//
// Admin,Auth area'larini dahil etmek icin yapilan islemler.
app.UseEndpoints(configure: endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Name}/{action=Index}/{id?}"
        );
    endpoints.MapAreaControllerRoute(
        name: "Account",
        areaName: "Account",
        pattern: "Account/{controller=Name}/{action=Index}/{id?}"
        );
    endpoints.MapDefaultControllerRoute();
});
//
//

app.Run();
