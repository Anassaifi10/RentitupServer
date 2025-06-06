using RentalApp.Application.Common.Models;

namespace RentalApp.Application.Common.Interfaces;
public interface IItemRequestService
{
    Task<Result> SendItemRequest(String itemId,string message,decimal rentLength);
    Task<Result> AcceptItemRequest(string requestId);
}
