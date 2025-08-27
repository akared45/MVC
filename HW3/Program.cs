using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "aptechStudentDetail",
    pattern: "aptech/student/{id}",
    defaults: new { controller = "Aptech", action = "StudentDetail" });

app.MapControllerRoute(
    name: "aptechStudent",
    pattern: "aptech/student",
    defaults: new { controller = "Aptech", action = "Student" });

app.MapControllerRoute(
    name: "aptech",
    pattern: "aptech",
    defaults: new { controller = "Aptech", action = "Index" });

app.MapControllerRoute(
    name: "vietnamCities",
    pattern: "Vietnam/thanhpho",
    defaults: new { controller = "Vietnam", action = "CityList" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();