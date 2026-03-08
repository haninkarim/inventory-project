using hanin.DBContext;
using hanin.Interceptors;
using hanin.Repositories;
using hanin.RepositoriesInterface;
using hanin.Service;
using hanin.ServiceIntefrace;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor(); // Needed for ITenantService

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<TenantInterceptor>();

builder.Services.AddScoped<ProductRepoInt, ProductRepo>();
builder.Services.AddScoped<CategoryRepoInt, CategoryRepo>();
builder.Services.AddScoped<ProductServiceInt, ProductService>();
builder.Services.AddScoped<CategoryServiceInt, CategoryService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    var interceptor = sp.GetRequiredService<TenantInterceptor>();

    // Using your hardcoded string for now (since it's a local project)
    var connectionString = "Server=127.0.0.1;Port=330;Database=hanin_db;Uid=root;Pwd=Hh123456.;SslMode=None;AllowPublicKeyRetrieval=True;";
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

    options.UseMySql(connectionString, serverVersion, mysqlOptions =>
        mysqlOptions.EnableRetryOnFailure())
           .AddInterceptors(interceptor); // Requirement 7 linked here!
});
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
