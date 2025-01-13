using WebCrud.BLL.Service;
using WebCrud.DAL.Helpers;
using WebCrud.DAL.Repositorie;
using WebCrud.DAL.Repository;
using WebCrud.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the ADO.NET database helper class for use in the application
builder.Services.AddSingleton<IDatabaseHelper>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    string? connectionString = configuration.GetConnectionString("stringConexionSQL");
    return new DatabaseHelper(connectionString ?? "Not connection available"); // DatabaseHelper class is responsible for database connection
});

// Register repository or other services that use ADO.NET

builder.Services.AddScoped<IGenericRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
