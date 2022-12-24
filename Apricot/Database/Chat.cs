using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class Chat
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ChannelID { get; set; }
        public string? ChatHash { get; set; }
        public string? Text { get; set; }
        public DateTime? Created { get; set; }
        public virtual TextChannel? Channel { get; set; }
    }
}
