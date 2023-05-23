using CentroCrud.Server.Models;
using Microsoft.EntityFrameworkCore;

//Referencia Token
using CentroCrud.Server.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Servicio de base de datos
builder.Services.AddDbContext<CentroFormacionContext>(opciones =>
{
  opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

//Cors
builder.Services.AddCors(opciones =>
{
  opciones.AddPolicy("PoliticaCors", app =>
  {
    app.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
  });
});

//Token
builder.Services.AddScoped<IAutorizationService, AutorizationService>();
var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
  config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
  config.RequireHttpsMetadata = false;
  config.SaveToken = true;
  config.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
  };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
//Token
app.UseAuthentication();

app.UseCors("PoliticaCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
