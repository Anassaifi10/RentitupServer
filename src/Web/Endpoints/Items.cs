using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using RentalApp.Application.Common.Models;
using RentalApp.Application.Items.Commands.CreateItem;
using RentalApp.Application.Items.Queries.GetItemDetails;
using RentalApp.Application.Items.Queries.GetItems;
using RentalApp.Domain.Entities;

namespace RentalApp.Web.Endpoints;

public class Items : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateItem, nameof(CreateItem))
            .MapGet(GetItem, nameof(GetItem))
            .MapGet(getItemInfo, "getItemInfo/{itemId}");
    }

    public async Task<Results<Created<string>, BadRequest<Result>>> CreateItem(ISender sender, CreateItemCommand command)
    {
        var (result, id) = await sender.Send(command);
        return string.IsNullOrEmpty(id)
            ? TypedResults.BadRequest(result)
            : TypedResults.Created(string.Empty, id);
    }

    public async Task<IResult> GetItem(ISender sender)
    {
        var (result, Items) = await sender.Send(new GetItemsQuery());

        if (result.Succeeded)
        {
            return TypedResults.Ok(new {
                Items,error="",
                success=true
            });
        }

        return TypedResults.BadRequest(new
        {
            success=false,
            error = result.Errors[0]
        }); // or NotFound(), depending on your logic
    }

    public async Task<IResult> getItemInfo(ISender sender,string itemId)
    {
        var (result, items) = await sender.Send(new GetItemDetailsQuery() { ItemId =itemId});

        if (result.Succeeded)
        {
            return TypedResults.Ok(new
            {
                items,
                Succeeded=true,
            });
        }
        return TypedResults.BadRequest(result);
    }
}
