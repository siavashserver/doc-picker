using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Reservations.Core.DataAccess.Entities;

namespace Services.Reservations.Core.DataAccess.EntityConfigurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasIndex(entity => new { entity.PatientId, entity.Date, entity.Shift }).IsUnique();
    }
}