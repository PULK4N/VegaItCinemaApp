using CinemaAppServices.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaAppPersistence.Configuration
{
    internal class TicketConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable(nameof(Reservation));

            builder.HasKey(reservation => reservation.Id);

            builder.Property(reservation => reservation.Id).ValueGeneratedOnAdd();

            builder.Property(reservation => reservation.TimeOfBuying).HasDefaultValue(DateTime.Now);

            builder.Property(reservation => reservation.Price).IsRequired();

        }
    }
}
