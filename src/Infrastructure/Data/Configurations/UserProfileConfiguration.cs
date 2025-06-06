using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalApp.Domain.Entities;

namespace RentalApp.Infrastructure.Data.Configurations;
public class UserProfileConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x=>x.FirstName).IsRequired();
    }
}
