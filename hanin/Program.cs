using hanin.DBContext;
using hanin.Repositories;
using hanin.RepositoriesInterface;
using hanin.Service;
using hanin.ServiceIntefrace;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = "Server=127.0.0.1;Port=330;Database=hanin_db;Uid=root;Pwd=Hh123456.;SslMode=None;AllowPublicKeyRetrieval=True;";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion, MySqlOptions =>
        MySqlOptions.EnableRetryOnFailure())); 
builder.Services.AddScoped<ProductServiceInt, ProductService>();
builder.Services.AddScoped<ProductRepoInt, ProductRepo>();
builder.Services.AddScoped<CategoryRepoInt, CategoryRepo>();
builder.Services.AddScoped<CategoryServiceInt, CategoryService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
