using Microsoft.EntityFrameworkCore;
using NZWal.API.Models.Domain;

namespace NZWal.API.Data
{
    public class NZWalDbContext: DbContext
    {
        public NZWalDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
