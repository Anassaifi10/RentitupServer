
using Microsoft.AspNetCore.Mvc;
using RentalApp.Application.Common.Models;
using RentalApp.Application.Image.Commands.UplodeImage;

namespace RentalApp.Web.Endpoints;

public class Image : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
              .RequireAuthorization()
              .MapPost(uplodeImage, nameof(uplodeImage));
    }
    public Task<Result> uplodeImage(string imageInfo, [FromForm] IFormFile image, ISender sender)
    {
        return sender.Send(new UplodeImageCommand()
        {
            Image = image,
            imageInfo = imageInfo,
        });
    }
}
