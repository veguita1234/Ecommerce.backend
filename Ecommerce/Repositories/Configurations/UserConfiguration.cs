using Ecommerce.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Repositories.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<SeguridadUsers>
    {
        public void Configure(EntityTypeBuilder<SeguridadUsers> builder)
        {
            builder.ToTable("SeguridadUsers");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.UserCode).HasColumnName("UserCode");
            builder.Property(x => x.Password).HasColumnName("Password");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.LastName).HasColumnName("LastName");
            builder.Property(x => x.Correo).HasColumnName("Corre");
            builder.Property(x => x.UserName).HasColumnName("UserName");
        }

        public void Configure(EntityTypeBuilder<SeguridadOptions> builder)
        {
            builder.ToTable("SeguridadOptions");
            builder.HasKey(x => x.OptionId);
            builder.Property(x => x.OptionId).HasColumnName("OptionId");
            builder.Property(x => x.OptionCode).HasColumnName("OptionCode");
            builder.Property(x => x.DescriptionCode).HasColumnName("DescriptionCode");
        }

        public void Configure(EntityTypeBuilder<SeguridadPermissions> builder)
        {
            builder.ToTable("SeguridadPermissions");
            builder.HasKey(x => x.PermissionId);
            builder.Property(x => x.PermissionId).HasColumnName("PermissionId");
            builder.Property(x => x.PermissionCode).HasColumnName("PermissionCode");
            builder.Property(x => x.DescriptionPermission).HasColumnName("DescriptionPermission");
        }

        public void Configure(EntityTypeBuilder<SeguridadProfiles> builder)
        {
            builder.ToTable("SeguridadProfiles");
            builder.HasKey(x => x.ProfileId);
            builder.Property(x => x.ProfileId).HasColumnName("ProfileId");
            builder.Property(x => x.ProfileCode).HasColumnName("ProfileCode");
            builder.Property(x => x.DescriptionProfile).HasColumnName("DescriptionProfile");
        }

        public void Configure(EntityTypeBuilder<SeguridadProfilesOptions> builder)
        {
            builder.ToTable("SeguridadProfilesOptions");
            builder.HasKey(x => x.ProfileOptionId);
            builder.Property(x => x.ProfileOptionId).HasColumnName("ProfileOptionId");
            builder.Property(x => x.ProfileId).HasColumnName("ProfileId");
            builder.Property(x => x.OptionId).HasColumnName("OptionId");
        }

        public void Configure(EntityTypeBuilder<SeguridadProfilesOptionsPermissions> builder)
        {
            builder.ToTable("SeguridadProfilesOptionsPermissions");
            builder.HasKey(x => x.ProfileOptionPermissionId);
            builder.Property(x => x.ProfileOptionPermissionId).HasColumnName("ProfileOptionPermissionId");
            builder.Property(x => x.ProfileOptionId).HasColumnName("ProfileOptionId");
            builder.Property(x => x.PermissionId).HasColumnName("PermissionId");
        }

        public void Configure(EntityTypeBuilder<SeguridadUsersOptionsPermissions> builder)
        {
            builder.ToTable("SeguridadUsersOptionsPermissions");
            builder.HasKey(x => x.UserOptionPermissionId);
            builder.Property(x => x.UserOptionPermissionId).HasColumnName("UserOptionPermissionId");
            builder.Property(x => x.UserOptionId).HasColumnName("UserOptionId");
            builder.Property(x => x.PermissionId).HasColumnName("PermissionId");
        }

        public void Configure(EntityTypeBuilder<SeguridadUsersOptions> builder)
        {
            builder.ToTable("SeguridadUsersOptions");
            builder.HasKey(x => x.UserOptionId);
            builder.Property(x => x.UserOptionId).HasColumnName("UserOptionId");
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.OptionId).HasColumnName("OptionId");
        }

        public void Configure(EntityTypeBuilder<SeguridadUsersProfiles> builder)
        {
            builder.ToTable("SeguridadUsersProfiles");
            builder.HasKey(x => x.UsserProfileId);
            builder.Property(x => x.UsserProfileId).HasColumnName("UsserProfileId");
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.ProfileId).HasColumnName("ProfileId");
        }
    }
}
