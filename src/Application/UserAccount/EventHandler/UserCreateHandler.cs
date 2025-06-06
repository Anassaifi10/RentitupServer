using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Domain.Constants;
using RentalApp.Domain.Events;

namespace RentalApp.Application.UserAccount.EventHandler;
public class UserCreateHandler(IApplicationDbContext context, IIdentityService identityService, ILogger<UserCreateHandler> logger) : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreateHandler> _logger = logger;
    private readonly IApplicationDbContext _context = context;
    private readonly IIdentityService _identityService = identityService;
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await _context.UserAccounts.AddAsync(new()
            {
                Id=notification.UserId,
                Email=notification.UserName,
                FirstName=notification.FirstName,
            });
            await _context.SaveChangesAsync(cancellationToken);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
        }
    }
}
