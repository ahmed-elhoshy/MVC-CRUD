using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVC_D8.Models;
using MVC_D8.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Register in DI container , so that we can use it in our controllers ... Key & Value

//builder.Services.AddTransient<IDeptRepo, DeptRepo>(); // Transient - new instance every time
//builder.Services.AddSingleton<IDeptRepo, DeptRepo>(); // Singleton - same instance every time even if relode page
builder.Services.AddScoped<IDeptRepo, DeptRepo>();     // Scoped - same instance in a single request (Mostly used)
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddDbContext<ITIMvcDbContext>(s => s.UseSqlServer(builder.Configuration.GetConnectionString("conStr1")), ServiceLifetime.Scoped);
//-----------------------------------------------------------------------------------------------------------------------------------------------------------
builder.Services.AddSession(s =>
s.Cookie.Name = "sessionID" //optional to change the name
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    s =>
{
    //s.LoginPath = "/department/index";
    //s.AccessDeniedPath = "/Home/AccessDenied";
    //s.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    //s.SlidingExpiration = true;
    //s.Cookie.Name = "authCookie"; //optional to change the name
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.Use(async (context ,next)=>
//{
//    await context.Response.WriteAsync("Hello from M1");   // print dah ayan kan eh
//    await next.Invoke();
//    await context.Response.WriteAsync("\nwelcome from M1 after ruturn from M2");
//});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("\nHello from M2"); 
//});


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
