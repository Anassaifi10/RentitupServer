using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalApp.Domain.Entities;

namespace RentalApp.Infrastructure.Data.Configurations;
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.Property(x=>x.Id).IsRequired().HasDefaultValueSql("NEWID()");
        builder.Property(x => x.Price).HasPrecision(10,2);
        builder.HasOne(x=>x.Owner).WithMany(x=>x.Items).HasForeignKey(x=>x.OwnerId).OnDelete(DeleteBehavior.Cascade);
    }
}
