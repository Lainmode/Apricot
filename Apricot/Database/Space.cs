using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class Space
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<SpaceUser>? SpaceUsers { get; set; }
        public DateTime VideoStart { get; set; }
        public VideoStatus VideoStatus { get; set; }
        public string? VideoUrl { get; set; }

        public int TextChannelID { get; set; }
        public virtual TextChannel TextChannel { get; set; }

    }

    public enum VideoStatus
    {
        Paused = 1,
        Playing = 2,
    }
}
