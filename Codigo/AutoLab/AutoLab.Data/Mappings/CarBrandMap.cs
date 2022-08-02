using AutoLab.Domain.Entities;
using AutoLab.Utils.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AutoLab.Data.Mappings
{
    public class CarBrandMap : MapBase<CarBrand>
	{
		public override void Configure(EntityTypeBuilder<CarBrand> builder)
		{
			builder.ToTable("CarBrand");

			builder.HasKey(c => c.Id);

			builder.Property(c => c.Brand)
				.IsRequired()
				.HasColumnName("Brand")
				.HasMaxLength(150);

			builder.Property(c => c.Removed)
				.IsRequired()
				.HasColumnName("Removed")
				.HasDefaultValue(false);

			builder.Property(c => c.Created)
				.IsRequired()
				.HasColumnName("Created")
				.HasDefaultValueSql("getdate()");

			builder.Property(c => c.Updated)
				.HasColumnName("Updated")
				.HasDefaultValueSql("getdate()");

		}
	}
}