using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class SpaceUser
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int SpaceID { get; set; }

        public virtual Space Space { get; set; }
        public virtual User User { get; set; }


    }
}
