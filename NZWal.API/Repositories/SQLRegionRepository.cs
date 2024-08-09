using Microsoft.EntityFrameworkCore;
using NZWal.API.Data;
using NZWal.API.Models.Domain;

namespace NZWal.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalDbContext dbContext;

        public SQLRegionRepository(NZWalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Region?> CreateAsync(Region model)
        {
            await dbContext.Regions.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return model;
        }
        public async Task<Region?> UpdateAsync(Guid id, Region model)
        {
            var res = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (res == null)
            {
                return null;
            }

            res.Code = model.Code;
            res.Name = model.Name;
            res.RegionImageUrl = model.RegionImageUrl;

            dbContext.SaveChanges();

            return res;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var res = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(res == null)
            {
                return null;
            }

            dbContext.Regions.Remove(res);
            await dbContext.SaveChangesAsync();
            return res;
        }
    }
}
