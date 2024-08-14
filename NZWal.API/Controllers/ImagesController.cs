using Microsoft.AspNetCore.Mvc;
using NZWal.API.Models.Domain;
using NZWal.API.Models.DTO;
using NZWal.API.Repositories;

namespace NZWal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST : api/Images/Upload
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                //Convert DTO to Domain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription,
                };

                //User repository to upload image
                await imageRepository.UploadImage(imageDomainModel);
                return Ok(imageDomainModel);


            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            //// Kiểm tra phần mở rộng tệp
            if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) == false)
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }

            //// Kiểm tra kích thước tệp
            if (request.File.Length > 10485760)// Tệp lớn hơn 10MB
            {
                ModelState.AddModelError("File", "file size more than 10MB, please upload a smaller size file .");
            }
        }
    }
}
