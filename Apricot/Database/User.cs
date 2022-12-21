using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Nickname { get; set; }
        public string? Picture { get; set; }
        public int Tag { get; set; }
        public string? StatusMessage { get; set; }
        public bool InActivity { get; set; }
        public Status Status { get; set; }
        public Activity Activity { get; set; }
        public virtual ICollection<Contact>? Contacts { get; set; }
        public virtual ICollection<SpaceUser>? SpaceUsers { get; set; }
    }


    public enum Status
    {
        None = 0,
        Offline = 1,
        Online = 2,
        Away = 3,
        DoNotDisturb = 4,
    }

    public enum Activity
    {
        None = 0,
        Watching = 1,
        Hosting = 2,
    }
}
