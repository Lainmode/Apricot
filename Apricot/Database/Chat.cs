using System.ComponentModel.DataAnnotations;

namespace Apricot.Database
{
    public class Chat
    {
        [Key]
        public int ChadID { get; set; }
        public int UserID { get; set; }
        public int UserID2 { get; set; }
        public string? ChatHash { get; set; }
        public string? Text { get; set; }
        public DateTime? Created { get; set; }
    }
}
