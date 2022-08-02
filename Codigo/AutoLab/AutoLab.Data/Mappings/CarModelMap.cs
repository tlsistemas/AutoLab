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

            builder.Property(c => c.IdCarBrand)
                .IsRequired()
                .HasColumnName("IdCarBrand");

            builder.Property(c => c.Model)
                .IsRequired()
                .HasColumnName("Model");

            builder.Property(c => c.Year)
                .IsRequired()
                .HasColumnName("Year");

        }
    }
}