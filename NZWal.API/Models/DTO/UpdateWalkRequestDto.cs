using System.ComponentModel.DataAnnotations;

namespace NZWal.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        [Required]
        public string? WalkImgeUrl { get; set; }
        [Required]
        public Guid DiffucultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
