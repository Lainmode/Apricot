using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class Space
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<User>? Users { get; set; }

    }
}
