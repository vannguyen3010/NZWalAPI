using NZWal.API.Models.Domain;

namespace NZWal.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region model);

        Task<Region?> UpdateAsync(Guid id, Region model);
        Task<Region?> DeleteAsync(Guid id);
    }
}
