using System.ComponentModel.DataAnnotations;

namespace NZWal.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Mã phải có tối thiểu 3 ký tự")]
        [MaxLength(3, ErrorMessage = "Mã phải có tối đa 3 ký tự")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tên phải có tối đa 100 ký tự")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
