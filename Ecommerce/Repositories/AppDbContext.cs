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
        public DbSet<SeguridadEmpresas> SeguridadEmpresas { get; set; }
        public DbSet<SeguridadEmpresasUsers> SeguridadEmpresasUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla SeguridadUsers
            modelBuilder.Entity<SeguridadUsers>()
                .ToTable("Users", "Seguridad");

            // Configuración de la tabla SeguridadOptions
            modelBuilder.Entity<SeguridadOptions>()
                .ToTable("SeguridadOptions", "Seguridad");

            // Configuración de la tabla SeguridadEmpresas
            modelBuilder.Entity<SeguridadEmpresas>()
                .ToTable("Empresas", "Seguridad");

            // Configuración de la tabla SeguridadEmpresasUsers
            modelBuilder.Entity<SeguridadEmpresasUsers>()
                .ToTable("Empresas_Users", "Seguridad")
                .HasKey(e => e.EmpresaUserId); // Define la clave primaria

            // Configuración de la relación entre SeguridadEmpresasUsers y SeguridadEmpresas
            modelBuilder.Entity<SeguridadEmpresasUsers>()
                .HasOne(eu => eu.SeguridadEmpresas)
                .WithMany(e => e.SeguridadEmpresasUsers) // Configura la navegación inversa
                .HasForeignKey(eu => eu.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación entre SeguridadEmpresasUsers y SeguridadUsers
            modelBuilder.Entity<SeguridadEmpresasUsers>()
                .HasOne(eu => eu.SeguridadUsers)
                .WithMany(u => u.SeguridadEmpresasUsers) // Configura la navegación inversa
                .HasForeignKey(eu => eu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}
