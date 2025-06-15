using Core.DTOs;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyedScoped<ICommonService<ProductDto, ProductInserDto, ProductUpdateDto>, ProductService>("productService");
builder.Services.AddKeyedScoped<ICommonService<UserDto, UserInserDto, UserUpdateDto>, UserService>("userService");
builder.Services.AddKeyedScoped<ICommonService<CategoryDto, CategoryInsertDto, CategoryUpdateDto>, CategoryService>("categoryService");

// EntityFramework
builder.Services.AddDbContext<SistemaVentasContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection"));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
