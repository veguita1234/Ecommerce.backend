using Ecommerce.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<SeguridadOptions> SeguridadOptions { get; set; }
        public DbSet<SeguridadPermissions> SeguridadPermissions { get; set; }
        public DbSet<SeguridadProfiles> SeguridadProfiles { get; set; }
        public DbSet<SeguridadProfilesOptions> SeguridadProfilesOptions { get; set; }
        public DbSet<SeguridadProfilesOptionsPermissions> SeguridadProfilesOptionsPermissions { get; set; }
        public DbSet<SeguridadUsers> SeguridadUsers { get; set; }
        public DbSet<SeguridadUsersOptions> SeguridadUsersOptions { get; set; }
        public DbSet<SeguridadUsersOptionsPermissions> SeguridadUsersOptionsPermissions { get; set; }
        public DbSet<SeguridadUsersProfiles> SeguridadUsersProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeguridadUsers>()
                .ToTable("Users", "Seguridad"); // Ajusta según tu esquema

            modelBuilder.Entity<SeguridadOptions>()
                .ToTable("SeguridadOptions");

            // Configuración adicional de otras entidades

            base.OnModelCreating(modelBuilder);
        }
    }
}
