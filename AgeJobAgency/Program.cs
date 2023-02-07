
using AgeJobAgency.Model;
using AgeJobAgency.Models;
using Microsoft.AspNetCore.Identity;
using AgeJobAgency.ViewModels;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.Configure<ReCAPTCHASettings>(builder.Configuration.GetSection("GooglereCAPTCHA"));
builder.Services.AddDataProtection();
builder.Services.AddTransient<GoogleCaptchaService>();
builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Login";
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache(); //save session in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
});



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
app.UseStatusCodePagesWithRedirects("/erros/{0}");
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();

