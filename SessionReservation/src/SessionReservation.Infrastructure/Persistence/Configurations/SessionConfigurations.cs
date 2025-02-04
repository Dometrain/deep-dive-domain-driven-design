using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.SessionAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SessionReservation.Api.Persistence.Configurations;

public class SessionConfigurations : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.TrainerId);

        builder.OwnsMany<Reservation>("_reservations", rb =>
        {
            rb.ToTable("SessionReservations");

            rb.HasKey(r => r.Id);

            rb.Property(r => r.Id)
                .ValueGeneratedNever();

            rb.WithOwner().HasForeignKey("SessionId");

            rb.Property(r => r.ParticipantId);
        });

        builder.OwnsMany(s => s.Categories, cb =>
        {
            cb.ToTable("SessionCategories");

            cb.WithOwner().HasForeignKey("SessionId");
            cb.HasKey("Id");

            cb.Property(c => c.Name);
            cb.Property(c => c.Value);
        })
        .Metadata
        .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(s => s.MaxParticipants);

        builder.Property(s => s.Date);

        builder.OwnsOne(s => s.Time);

        builder.Property(s => s.Name);
        builder.Property(s => s.Description);
        builder.Property(s => s.RoomId);
    }
}