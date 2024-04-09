using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartApplication.Data;
using ShoppingCartApplication.Mappers;
using ShoppingCartApplication.Migrations;
using ShoppingCartApplication.Repositories;
using ShoppingCartApplication.Service;
using ShoppingCartApplication.ServiceContract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpContextAccessor(); // To use HttpContext.Session in other classes which are not controllers

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddScoped<CartRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession(); // Use session middleware

//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    context.Response.
//    if (context.Session.GetString("UserEmail") == null)
//    {
//        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//        await context.Response.WriteAsync("User is not authenticated");
//    }
//    await next(context);
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
