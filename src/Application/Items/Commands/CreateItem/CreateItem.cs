using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;
using RentalApp.Domain.Enums;

namespace RentalApp.Application.Items.Commands.CreateItem;

public record CreateItemCommand : IRequest<(Result,string)>
{
    public string ItemName { get; set; } = "";
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemPriceType PriceType { get; set; } 
}

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
    }
}

public class CreateItemCommandHandler(IApplicationDbContext context, IItemServices itemServices) : IRequestHandler<CreateItemCommand, (Result,string)>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IItemServices _itemServices = itemServices;

    public async Task<(Result, string)> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        return await _itemServices.createItem(new CreateItemVm()
        {
            Description = request.Description,
            ItemName = request.ItemName,
            Price = request.Price,
            PriceType=request.PriceType
        });
    }
}
