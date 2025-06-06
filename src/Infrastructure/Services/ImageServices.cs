using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Infrastructure.Services;
public class ImageServices : IImageServices
{
    private readonly Cloudinary _cloudinary;
    private readonly IApplicationDbContext _context;

    public ImageServices(IOptions<CloudinarySetting> config, IApplicationDbContext context)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);

        _cloudinary = new Cloudinary(acc);
        _context = context;

    }
    public async Task<Result> UplodeImage(IFormFile file, string foldername, string Id)
    {

        try
        {
            foldername = foldername.ToLower();

            string filePath = foldername + "/" + Id;
            var deleteParams = new DeletionParams(filePath);
            await _cloudinary.DestroyAsync(deleteParams);

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = filePath,
                Overwrite = true
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            //var entity; 

            if (foldername == "user")
            {
                var user = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == Id);
                if (user == null)
                {
                    return Result.Failure(["Invalid Id for Uploding the Image"]);
                }
                user.Profileimage = result.Url.ToString();
                _context.UserAccounts.Update(user);
            }
            else
            {
                var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == Id);
                if (item == null)
                {
                    return Result.Failure(["Invalid Id for Uploding the Image"]);
                }
                item.Imageurl = result.Url.ToString();
                _context.Items.Update(item);
            }

            await _context.SaveChangesAsync(CancellationToken.None);
            return Result.Success();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
