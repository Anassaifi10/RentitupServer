using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalApp.Domain.Entities;

namespace RentalApp.Infrastructure.Data.Configurations;
public class RentalRequestConfiguration : IEntityTypeConfiguration<RentalRequest>
{
    public void Configure(EntityTypeBuilder<RentalRequest> builder)
    {

        builder.Property(x => x.Id).IsRequired().HasDefaultValueSql("NEWID()");
        builder.Property(x => x.ItemId).IsRequired(false);
        builder.Property(x => x.RentTimeLength).HasPrecision(10, 2);
        builder.HasOne(x => x.Item).WithMany(x => x.RentalRequests).HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.RentalRequestUser).WithMany(x => x.RentalRequests).HasForeignKey(x => x.RenterId).OnDelete(DeleteBehavior.Cascade);
    }
}
