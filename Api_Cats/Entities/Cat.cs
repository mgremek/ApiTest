using System.ComponentModel.DataAnnotations;

namespace Api_Cats.Entities
{    
    public class Cat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
