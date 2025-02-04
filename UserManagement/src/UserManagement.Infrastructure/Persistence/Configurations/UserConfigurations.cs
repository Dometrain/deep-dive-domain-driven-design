using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UserManagement.Api.Models;

namespace UserManagement.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName);

        builder.Property(u => u.LastName);

        builder.Property(u => u.Email);

        builder.Property(u => u.AdminId);

        builder.Property(u => u.ParticipantId);

        builder.Property(u => u.TrainerId);

        builder.Property("_passwordHash")
            .HasColumnName("PasswordHash");
    }
}
