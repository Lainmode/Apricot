using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apricot.Database
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("User2")]
        public int UserID2 { get; set; }
        [NotMapped]
        public virtual User? User { get; set; }
        public virtual User? User2 { get; set; }
    }
}
