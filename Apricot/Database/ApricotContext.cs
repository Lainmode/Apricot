using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace Apricot.Database
{
    public class ApricotContext : DbContext
    {
        public const string connectionString = "Server=localhost\\SQLEXPRESS;Database=Apricot;Trusted_Connection=True;TrustServerCertificate=True;";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }




        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Space> Spaces { get; set; }
        public virtual DbSet<SpaceUser> SpaceUsers { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<TextChannel> TextChannels { get; set; }

    }
}
