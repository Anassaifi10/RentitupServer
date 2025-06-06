using RentalApp.Domain.Entities;

namespace RentalApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
   
    DbSet<Domain.Entities.UserAccount> UserAccounts { get; }

    DbSet<Rental> Rentals { get; }

    DbSet<RentalRequest> RentalRequests { get; }

    DbSet<Item> Items { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
