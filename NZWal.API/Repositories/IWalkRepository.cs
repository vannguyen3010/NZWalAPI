using NZWal.API.Models.Domain;

namespace NZWal.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk model);

        Task<List<Walk>> GetAllWalksAsync();

        Task<Walk?> GetWalkByIdAsync(Guid id);

        Task<Walk?> UpdateWalkAsync(Guid id, Walk model);

        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
