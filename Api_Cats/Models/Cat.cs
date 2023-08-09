namespace Api_Cats.Models
{
    public interface ICat
    {
        string Sound();
    }
    public class Cat : ICat
    {
        public string Sound() => "Meoooow";
    }
}
