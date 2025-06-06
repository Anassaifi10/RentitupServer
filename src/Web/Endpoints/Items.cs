using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentalApp.Application.Common.Models;
using RentalApp.Application.Items.Commands.CreateItem;

namespace RentalApp.Web.Endpoints;

public class Items : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateItem, nameof(CreateItem));
    }

    public async Task<Results<Created<string>, BadRequest<Result>>> CreateItem(ISender sender,  CreateItemCommand command)
    {
        var (result, id) = await sender.Send(command);
        return string.IsNullOrEmpty(id)
            ? TypedResults.BadRequest(result)
            : TypedResults.Created(string.Empty, id);
    }
}
