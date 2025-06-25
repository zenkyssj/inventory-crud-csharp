using Core.DTOs;
using Core.Models;
using Core.Models.Common;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyedScoped<ICommonService<ProductDto, ProductInserDto, ProductUpdateDto>, ProductService>("productService");

builder.Services.AddKeyedScoped<ICommonService<UserDto, UserInserDto, UserUpdateDto>, UserService>("userService");
builder.Services.AddKeyedScoped<ICommonService<CategoryDto, CategoryInsertDto, CategoryUpdateDto>, CategoryService>("categoryService");
builder.Services.AddKeyedScoped<ICommonService<VentaDto, VentaInsertDto, VentaUpdateDto>, VentaService>("ventaService");

builder.Services.AddKeyedScoped<IConceptService<ConceptDto>, ConceptService>("conceptService");
builder.Services.AddKeyedScoped<IReportService<ProductDto>, ConceptService>("conceptReportService");

builder.Services.AddKeyedScoped<ILoginService, LoginService>("loginService");


// EntityFramework
builder.Services.AddDbContext<SistemaVentasContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection"));
});

// JWT
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(d =>
  {
      d.RequireHttpsMetadata = false;
      d.SaveToken = true;
      d.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          RoleClaimType = ClaimTypes.Role
      };
  });



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // Para usar autenticacion con JWT

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
