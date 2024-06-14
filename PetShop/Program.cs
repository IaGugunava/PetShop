using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PetShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetCS"));
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(Mapper));

var tokenKey = builder.Configuration["AppSettings:Token"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var key = builder.Configuration["AppSettings:Token"];
    if (string.IsNullOrEmpty(key))
    {
        throw new InvalidOperationException("Invalid Token");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
