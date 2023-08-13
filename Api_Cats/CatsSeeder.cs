using Api_Cats.Entities;

namespace Api_Cats
{
    public class CatsSeeder
    {
        private readonly CatsDbContext _dbContext;

        public CatsSeeder(CatsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Cats.Any())
            {
                var cats = new List<Cat>()
                {
                    new Cat() {Name="Tymon", Color="Silver tabby"},
                    new Cat() {Name="Gabryś", Color="Black tabby"}
                };

                _dbContext.Cats.AddRange(cats);
                _dbContext.SaveChanges();
            }
        }
    }
}
