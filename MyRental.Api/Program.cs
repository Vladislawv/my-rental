using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyRental.Api.Middlewares;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;
using MyRental.Infrastructure.Seeders;
using MyRental.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config => config.Filters.Add<ValidateModelAttribute>());

builder.Services.AddFluentValidation(configuration =>
    configuration.RegisterValidatorsFromAssembly(typeof(ValidationRuleBuilderExtensions).Assembly));

builder.Services.AddDbContext<MyRentalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyRentalDatabase")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MyRentalContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(EntityDto));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MyRental API",
        Description = "An ASP.NET Core Web API for MyRental app"
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>() 
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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

builder.Services.AddTransient<IRoleService, RoleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();