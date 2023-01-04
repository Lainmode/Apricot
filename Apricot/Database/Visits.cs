using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class Visit
    {
        [Key]
        public int ID { get; set; }
        public string? IPAddress { get; set; }
        public string? Route { get; set; }
        public string? Protocol { get; set; }
        public string? Method { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
