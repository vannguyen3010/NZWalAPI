using NZWal.API.Models.Domain;

namespace NZWal.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk model);

        Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk?> GetWalkByIdAsync(Guid id);

        Task<Walk?> UpdateWalkAsync(Guid id, Walk model);

        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
