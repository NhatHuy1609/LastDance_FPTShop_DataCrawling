using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace database_api.Data.MappingConfigurations
{
    public class MonitorConfiguration : IEntityTypeConfiguration<Entities.Monitor>
    {
        public void Configure(EntityTypeBuilder<Entities.Monitor> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Url).IsRequired().HasMaxLength(255);
            builder.Property(m => m.ImageUrl).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Price).IsRequired(false);
            builder.Property(m => m.PriceDiscount).IsRequired(false);
            builder.Property(m => m.Category).IsRequired(false);
            builder.Property(m => m.isAvailable).IsRequired(false);
        }
    }
}