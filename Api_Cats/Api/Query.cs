using Api_Cats.Entities;
using Api_Cats.Services;
using System.Numerics;

namespace Api_Cats.Api
{
    public class Query
    {
        public async Task<IEnumerable<Cat>> GetCatsAsync([Service] ICatsQlService catsService)
        {
            return await catsService.GetCatsAsync();
        }
    }
}
