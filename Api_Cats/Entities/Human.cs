using System.ComponentModel.DataAnnotations;

namespace Api_Cats.Entities
{
    public class Human
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Cat> Cats { get; set; }
    }
}
