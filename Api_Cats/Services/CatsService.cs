using Api_Cats.Entities;
using System.Security.Cryptography;

namespace Api_Cats.Services
{
    public interface ICatsService
    {
        IEnumerable<Cat> GetAll();
        Cat Get(int id);
        void Delete(int id);
        int Create(Cat cat);
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
            var human = _catsDbContext.Cats.FirstOrDefault(x => x.Id == id);

            if (human is not null)
                _catsDbContext.Cats.Remove(human);

            return;
        }


        public int Create(Cat cat)
        {
            _catsDbContext.Cats.Add(cat);
            return cat.Id;
        }
        public void Modify()
        {

        }
    }
}