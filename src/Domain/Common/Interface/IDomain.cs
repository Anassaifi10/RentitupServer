namespace RentalApp.Domain.Common.Interface;
public interface IDomain
{

    IReadOnlyCollection<BaseEvent> DomainEvents { get; }

    void AddDomainEvent(BaseEvent baseEvent);

    void RemoveDomainEvent(BaseEvent baseEvent);

    void ClearDomainEvents();


}
