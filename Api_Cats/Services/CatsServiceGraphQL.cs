using Api_Cats.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Api_Cats.Services
{
    public interface ICatsServiceQL
    {
        Task<IEnumerable<Cat>> GetCatsAsync();
    }
    public class CatsServiceGraphQL : ICatsServiceQL
    {
        private readonly CatsDbContext _dbContext;

        public CatsServiceGraphQL(CatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Cat>> GetCatsAsync()
        {
            return await _dbContext.Cats.ToListAsync();
        }
    }
}
