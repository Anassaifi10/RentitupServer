using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalApp.Domain.Entities;

namespace RentalApp.Infrastructure.Data.Configurations;
public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.Property(x=>x.Id).IsRequired().HasDefaultValueSql("NEWID()");
        builder.HasOne(x=>x.RentalRequest).WithOne(x=>x.Rental).HasForeignKey<Rental>(x=>x.requestId).OnDelete(DeleteBehavior.Restrict);
    }
}
