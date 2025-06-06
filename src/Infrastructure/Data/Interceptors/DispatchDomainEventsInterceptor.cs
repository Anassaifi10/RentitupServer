using RentalApp.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RentalApp.Infrastructure.Identity;

namespace RentalApp.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DispatchDomainEventsInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        DispatchApplicationEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);

    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        await DispatchApplicationEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<BaseEntity<int>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);
    }

    public async Task DispatchApplicationEvents(DbContext? context)
    {
        if (context==null)
        {
            return;
        }

        var entities = context.ChangeTracker.Entries<ApplicationUser>()
            .Where(e=>e.Entity.DomainEvents.Any()).Select(e=>e.Entity);

        var ApplicationEvents=entities.SelectMany(e=>e.DomainEvents).ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach(var applicationevent in ApplicationEvents)
        {
            await _mediator.Publish(applicationevent);
        }
    }
}
