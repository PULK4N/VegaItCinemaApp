using CinemaAppServices.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppPersistence.Configuration
{
    internal class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable(nameof(Seat));

            builder.HasKey(seat => seat.Id);

            builder.HasIndex(seat => new { seat.Row, seat.Column, seat.MovieScreeningId }).IsUnique();

            builder.Property(seat => seat.Id).ValueGeneratedOnAdd();

            builder.Property(seat => seat.Row).IsRequired();

            builder.Property(seat => seat.Column).IsRequired();

            //builder.HasOne(seat => seat.MovieScreening)
            //    .WithOne()
            //    .HasForeignKey(movieScreening => movieScreening.Id)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
