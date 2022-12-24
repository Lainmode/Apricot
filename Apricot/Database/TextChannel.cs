using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class TextChannel
    {
        [Key]
        public int ID { get; set; }
        public string? GUID { get; set; }
        public ChannelType ChannelType { get; set; }
        public virtual ICollection<Chat>? Chats { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }

    public enum ChannelType
    {
        PrivateChannel = 1,
        GroupChannel = 2,
    }
}
