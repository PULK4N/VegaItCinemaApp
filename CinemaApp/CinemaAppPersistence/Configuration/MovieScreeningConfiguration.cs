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
    internal class MovieScreeningConfiguration : IEntityTypeConfiguration<MovieScreening>
    {
        public void Configure(EntityTypeBuilder<MovieScreening> builder)
        {
            builder.ToTable(nameof(MovieScreening));

            builder.HasKey(movieScreening => movieScreening.Id);

            builder.Property(movieScreening => movieScreening.Id).ValueGeneratedOnAdd();

            builder.Property(movieScreening => movieScreening.StartTime).IsRequired();

            builder.Property(movieScreening => movieScreening.NumOfRows).IsRequired();

            builder.Property(movieScreening => movieScreening.NumOfColumns).IsRequired();

        }
    }
}
