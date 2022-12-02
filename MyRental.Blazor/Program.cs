using System.Text;
using Blazored.LocalStorage;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyRental.Blazor.Authentication;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;
using MyRental.Infrastructure.Seeders;
using MyRental.Services;
using MyRental.Services.Areas.Advertisements;
using MyRental.Services.Areas.Medias;
using MyRental.Services.Areas.Notifications;
using MyRental.Services.Areas.Users;
using MyRental.Services.Areas.Users.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, MyAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddFluentValidation(configuration =>
    configuration.RegisterValidatorsFromAssembly(typeof(ValidationRuleBuilderExtensions).Assembly));

builder.Services.AddDbContext<MyRentalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyRentalDatabase")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MyRentalContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(EntityDto));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddTransient<ISeeder<Role>, RolesSeeder>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IAdvertisementService, AdvertisementService>();

builder.Services.AddTransient<IMediaService, MediaService>();

builder.Services.AddTransient<INotificationService, NotificationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAuthorization();
app.UseAuthentication();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();