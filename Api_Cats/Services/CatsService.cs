using Api_Cats.Entities;
namespace Api_Cats.Services
{
    public interface ICatsService
    {
        IEnumerable<Cat> GetAll();
        Cat Get(int id);
        void Delete(int id);
        int Create(Cat cat);
        void Update(int id, Cat cat);
    }

    public class CatsService : ICatsService
    {
        private readonly CatsDbContext _catsDbContext;

        public CatsService(CatsDbContext catsDbContext)
        {
            _catsDbContext = catsDbContext;
        }

        public IEnumerable<Cat> GetAll() => _catsDbContext.Cats.ToList();

        public Cat Get(int id) => _catsDbContext.Cats.FirstOrDefault(x => x.Id == id);

        public void Delete(int id)
        {
            var cat = _catsDbContext.Cats.FirstOrDefault(x => x.Id == id);

            if (cat is not null)
            {
                _catsDbContext.Cats.Remove(cat);
                _catsDbContext.SaveChanges(); 
            }

            return;
        }

        public int Create(Cat cat)
        {
            _catsDbContext.Cats.Add(cat);
            _catsDbContext.SaveChanges();

            return cat.Id;
        }

        public void Update(int id, Cat cat)
        {
            var c = _catsDbContext.Cats.FirstOrDefault(x => x.Id == id);

            if (c is not null)
            {
                _catsDbContext.Cats.Remove(c);
                _catsDbContext.Cats.Add(cat);
                _catsDbContext.SaveChanges();
            }

            return;
        }
    }
}