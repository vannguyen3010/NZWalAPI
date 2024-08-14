using NZWal.API.Models.Domain;

namespace NZWal.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image model);
    }
}
