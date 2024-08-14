using Microsoft.EntityFrameworkCore;
using NZWal.API.Data;
using NZWal.API.Models.Domain;

namespace NZWal.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalDbContext dbContext;

        // IHttpContextAccessor Khi link vào ảnh thì sẽ tạo ra url ảnh https://localhost:1234/images/image.png
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> UploadImage(Image model)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{model.FileName}{model.FileExtension}");

            //Upload Image to local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await model.File.CopyToAsync(stream);

            //https://localhost:1234/images/image.png

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{model.FileName}{model.FileExtension}";
        
            model.FilePath = urlFilePath;

            //Add Image to the Images table
            await dbContext.Images.AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model;
        }
    }
}
