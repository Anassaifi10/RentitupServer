using Microsoft.AspNetCore.SignalR;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.ItemRequest.Commands.SendItemRequest;

namespace RentalApp.Application.Common;
public class RentalHub(ISender sender):Hub<IRentalHub>
{
    private readonly ISender _sender=sender;

    public async Task InvokeSendRequest(string ItemId,string message,decimal RentalLength)
    {
        SendItemRequestCommand command = new SendItemRequestCommand();
        command.ItemId = ItemId;
        command.message = message;
        command.RentalLength = RentalLength;


        await _sender.Send(command);
    }

}
