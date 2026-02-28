using hanin.Controller;
using hanin.Entities;
using Microsoft.EntityFrameworkCore;

namespace hanin.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<ProductEntity> ProductEntity { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
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
