using CinemaWil.Configuration;
using CinemaWilWeb1.Configuration;
using CinemaWilWeb1.Interfase;
using CinemaWilWeb1.Services;
using Domain.Interface;
using Domain.Interface.JWT;
using Infractructure.Data;
using Infractructure.Services;
using Infractructure.Services.JWT;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));


builder.Services.GetDependencyInjectionsWeb();
builder.Services.GetDependencyInjections();


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

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginCreate}/{id?}");

app.Run();
