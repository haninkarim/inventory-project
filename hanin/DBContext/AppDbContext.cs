using hanin.Controller;
using hanin.Entities;
using hanin.ServiceIntefrace;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hanin.DBContext
{
    public class AppDbContext : DbContext
    {
        private readonly ITenantService _tenantService;
        // public AppDbContext(DbContextOptions options):base(options)
        // {
        // }
        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantService? tenantService = null)
 : base(options)
        {
            _tenantService = tenantService;
        }
        public DbSet<ProductEntity> ProductEntity { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Price).HasPrecision(18, 2);

                entity.Property(e => e.CategoryId).IsRequired();
            });
            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ProductEntity>()
                .HasOne(p => p.Category)      
                .WithMany(c => c.Products)   
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            var currentTenantId = _tenantService?.GetTenantId();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");

                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, nameof(EntityBase.TenantId)),
                            Expression.Constant(currentTenantId)
                        ),
                        parameter
                    );

                  
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=127.0.0.1;Port=330;Database=hanin_db;Uid=root;Pwd=Hh123456.;SslMode=None;AllowPublicKeyRetrieval=True;";
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)));
            }
        }
    }
}
