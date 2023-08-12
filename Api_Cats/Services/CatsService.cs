using Api_Cats.Entities;

namespace Api_Cats.Services
{
    public interface ICatsService
    {
        IEnumerable<Cat> GetAll();
    }

    public class CatsService : ICatsService
    {
        public IEnumerable<Cat> GetAll() => new List<Cat>();
    }
}