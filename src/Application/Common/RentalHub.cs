using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.ItemRequest.Commands.SendItemRequest;

namespace RentalApp.Application.Common;
[Authorize]
public class RentalHub(ISender sender,IUser user):Hub<IRentalHub>
{
    private readonly ISender _sender=sender;
    private readonly IUser _user= user;

    public async Task InvokeSendRequest(string ItemId,string message,decimal RentalLength)
    {
        SendItemRequestCommand command = new SendItemRequestCommand();
        command.ItemId = ItemId;
        command.message = message;
        command.RentalLength = RentalLength;


        await _sender.Send(command);
    }

    public async Task InvokeTestMessage(string message,string reciveId)
    {
       await Clients.User(reciveId).ReceiveMessage(message);
        await Clients.User(_user.Id).SendMessageSuccessfully();
    }

}
