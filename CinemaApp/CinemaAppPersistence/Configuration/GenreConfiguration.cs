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
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable(nameof(Genre));

            builder.HasKey(genre => genre.Id);

            builder.Property(genre => genre.Id).ValueGeneratedOnAdd();

            builder.Property(genre => genre.Name).HasMaxLength(40).IsRequired();

        }
    }
}
