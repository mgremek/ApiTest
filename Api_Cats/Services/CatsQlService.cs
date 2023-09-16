using Api_Cats.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Api_Cats.Services
{
    public interface ICatsQlService
    {
        Task<IEnumerable<Cat>> GetCatsAsync();
    }
    public class CatsQLService : ICatsQlService
    {
        private readonly CatsDbContext _context;

        public CatsQLService(CatsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cat>> GetCatsAsync()
        {
            return await _context.Cats
               .ToListAsync();
        }
    }

}
