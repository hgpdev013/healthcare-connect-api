using Microsoft.EntityFrameworkCore;
using apihealthcareconnect.Models;

namespace apihealthcareconnect.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<SpecialtyType> SpecialtyType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionString = "server=localhost; database=healthcare_connect; user=root; password=";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
