using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using RentalApp.Domain.Common;
using RentalApp.Domain.Common.Interface;
using RentalApp.Domain.Entities;
using RentalApp.Domain.Events;

namespace RentalApp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser ,IDomain
{
    public UserAccount UserAccuont { get; set; } = null!;
    public override string? UserName
    {
        get => base.UserName;
        set
        {
            base.UserName = value;
            if (!string.IsNullOrEmpty(value))
            {
                string firstName = value.Split('@')[0].ToUpper();
                AddDomainEvent(new UserCreatedEvent(Id, value, firstName));
            }
        }
    }

    private List<BaseEvent> _domainEvents { get; set; } = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent baseEvent)
    {
        _domainEvents.Add(baseEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RemoveDomainEvent(BaseEvent baseEvent)
    {
        _domainEvents.Remove(baseEvent);
    }
}
