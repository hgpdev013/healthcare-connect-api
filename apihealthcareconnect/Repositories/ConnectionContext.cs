using Microsoft.EntityFrameworkCore;
using apihealthcareconnect.Models;
using Microsoft.Extensions.Configuration;

namespace apihealthcareconnect.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ConnectionContext(IConfiguration configuration, DbContextOptions<ConnectionContext> options)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserTypePermissions> UserTypePermissions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<SpecialtyType> SpecialtyType { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Pacients> Pacients { get; set; }
        public DbSet<Allergies> Allergies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("HealthcareConnect");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
