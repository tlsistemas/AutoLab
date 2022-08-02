using AutoLab.Domain.Entities;
using AutoLab.Utils.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoLab.Data.Mappings
{
    public class CarModelMap : MapBase<CarModel>
    {
        public override void Configure(EntityTypeBuilder<CarModel> builder)
        {
            builder.ToTable("CarModel");

            builder.Property(c => c.CarBrandId)
                .IsRequired()
                .HasColumnName("CarBrandId");

            builder.Property(c => c.Model)
                .IsRequired()
                .HasColumnName("Model")
                .HasMaxLength(150);

            builder.Property(c => c.Year)
                .IsRequired()
                .HasColumnName("Year");

            builder.Property(c => c.Removed)
                .IsRequired()
                .HasColumnName("Removed")
                .HasDefaultValue(false);

            builder.Property(c => c.Created)
                .IsRequired()
                .HasColumnName("Created")
                .HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Updated)
                .HasColumnName("Updated")
                .HasDefaultValue(DateTime.Now);

        }
    }
}