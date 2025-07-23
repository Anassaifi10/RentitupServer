using Microsoft.AspNetCore.Http;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.Image.Commands.UplodeImage;

public record UplodeImageCommand : IRequest<Result>
{
    public string? imageInfo { get; set; }
    public IFormFile? Image { get; set; }
}

public class UplodeImageCommandValidator : AbstractValidator<UplodeImageCommand>
{
    public UplodeImageCommandValidator()
    {
    }
}

public class UplodeImageCommandHandler(IApplicationDbContext context, IImageServices imageServices) : IRequestHandler<UplodeImageCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IImageServices _imageServices = imageServices;

    public async Task<Result> Handle(UplodeImageCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.imageInfo))
        {
            return Result.Failure(["Image info not Found"]);
        }
        
        if (request.Image == null)
        {
            return Result.Failure(["Image not found"]);
        }
        return await _imageServices.UplodeImage(request.Image, "useritem", request.imageInfo);

    }
}
