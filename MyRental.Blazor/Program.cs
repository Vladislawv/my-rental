using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;
using MyRental.Infrastructure.Seeders;
using MyRental.Services;
using MyRental.Services.Areas.Ads.Services;
using MyRental.Services.Areas.Users.Services;
using MyRental.Services.Areas.Users.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddFluentValidation(configuration =>
    configuration.RegisterValidatorsFromAssembly(typeof(ValidationRuleBuilderExtensions).Assembly));

builder.Services.AddDbContext<MyRentalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyRentalDatabase")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MyRentalContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAutoMapper(typeof(EntityDto));

builder.Services.AddTransient<ISeeder<Role>, RolesSeeder>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IAdService, AdService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();