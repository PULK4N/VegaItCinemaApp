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
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable(nameof(Movie));

            builder.HasKey(movie => movie.Id);

            builder.Property(movie => movie.Id).ValueGeneratedOnAdd();

            builder.Property(movie => movie.Name).HasMaxLength(100).IsRequired();

            builder.Property(movie => movie.OriginalName).HasMaxLength(100).IsRequired();

            builder.Property(movie => movie.DurationMinutes).IsRequired();

        }
    }
}
