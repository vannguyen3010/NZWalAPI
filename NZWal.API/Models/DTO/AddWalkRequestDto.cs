namespace NZWal.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImgeUrl { get; set; }
        public Guid DiffucultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
