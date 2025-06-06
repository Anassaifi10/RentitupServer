namespace RentalApp.Application.Common.Interfaces;
using Microsoft;
using Microsoft.AspNetCore.Http;
using RentalApp.Application.Common.Models;

public interface IImageServices
{

    Task<Result> UplodeImage(IFormFile file,string foldername,string Id);
}
