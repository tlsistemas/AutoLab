using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLab.Utils.Bases
{
    public class MapBase<T> : IEntityTypeConfiguration<T> 
        where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("Id");
            builder.Property(c => c.Removed)
                .IsRequired()
                .HasColumnName("Removed").HasDefaultValue(false);

            builder.Property(c => c.Created)
                .IsRequired()
                .HasColumnName("Created").HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Updated)
                .HasColumnName("Updated").HasDefaultValue(DateTime.Now);
        }
    }
}
