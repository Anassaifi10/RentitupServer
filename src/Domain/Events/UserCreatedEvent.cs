using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Domain.Events;
public record UserCreatedEvent(string UserId, string UserName, string FirstName):BaseEvent;
