using MediatR;

namespace RentalApp.Domain.Common;

public abstract record BaseEvent : INotification;
