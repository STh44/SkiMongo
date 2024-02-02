using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Serilog;
using Ski_ServiceNoSQL.Models;
using Ski_ServiceNoSQL.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Logger
var loggerFromSettings = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .Enrich.FromLogContext()
               .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerFromSettings);

builder.Services.Configure<SkiDatabaseSettings>(
    builder.Configuration.GetSection(nameof(SkiDatabaseSettings)));

builder.Services.AddSingleton<ISkiDatabaseSettings>(sp =>
sp.GetRequiredService<IOptions<SkiDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("SkiDatabaseSettings:ConnectionString")));

builder.Services.AddSingleton<IOrdersService, OrdersService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IMitarbeiterService, MitarbeiterService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataProtection();

//
// JWT
//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
