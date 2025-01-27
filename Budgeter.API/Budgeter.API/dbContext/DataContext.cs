using Budgeter.API.Models.ADM;
using Budgeter.API.Models.CTL;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.API.dbContext
{
    public class DataContext : DbContext
    {
        private String connectionString = String.Empty;
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            var appsettings = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = appsettings.GetConnectionString("Budgeter_Context");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        // Usuarios
        public DbSet<Usuarios> Usuarios { get; set; }

        // Catálogos
        public DbSet<MetodosPagos> MetodosPagos { get; set; }
    }
}