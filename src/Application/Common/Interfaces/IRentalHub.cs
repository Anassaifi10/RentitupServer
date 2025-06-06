using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Common.Interfaces;
public interface IRentalHub
{
    Task SendRequest(string itemId);
    Task ReciveRequest(string message);
    Task SendRequestSuccessfully();

    Task AccepRequest();

    Task AccepRequestSuccessfully();
}
