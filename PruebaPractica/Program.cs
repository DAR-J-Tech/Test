using Microsoft.AspNetCore.Authentication.Cookies;
using PruebaPractica.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(45);

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login";
        option.LogoutPath = "/Logout";
        option.SlidingExpiration = true;
        option.ExpireTimeSpan = TimeSpan.FromMinutes(45);
        option.Cookie.MaxAge = option.ExpireTimeSpan;
    });
builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
