using hanin.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace hanin
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = "Server=127.0.0.1;Port=330;Database=hanin_db;Uid=root;Pwd=Hh123456.;SslMode=None;AllowPublicKeyRetrieval=True;";

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}