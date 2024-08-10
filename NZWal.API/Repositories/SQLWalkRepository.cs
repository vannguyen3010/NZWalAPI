using Microsoft.EntityFrameworkCore;
using NZWal.API.Data;
using NZWal.API.Models.Domain;

namespace NZWal.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalDbContext dbContext;

        public SQLWalkRepository(NZWalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk model)
        {
            await dbContext.Walks.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            //Lấy Csdl bảng Walks và 2 bảng Difficulty, Region
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk model)
        {
            var res = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                return null;
            }

            res.Name = model.Name;
            res.Description = model.Description;
            res.LengthInKm = model.LengthInKm;
            res.WalkImageUrl = model.WalkImageUrl;
            res.RegionId = model.RegionId;
            res.DifficultyId = model.DifficultyId;

            await dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var res = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                return null;
            }
            
            dbContext.Walks.Remove(res);
            await dbContext.SaveChangesAsync();
            return res;
        }
    }
}
