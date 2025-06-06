using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalApp.Domain.Entities;
using RentalApp.Infrastructure.Identity;

namespace RentalApp.Infrastructure.Data.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne((x)=>x.UserAccuont).WithOne().HasForeignKey<UserAccount>(x=>x.Id).OnDelete(DeleteBehavior.Cascade);
    }
}
