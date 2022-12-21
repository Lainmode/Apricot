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




        public DbSet<User> Users { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<SpaceUser> SpaceUsers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Chat> Chats { get; set; }
    }
}
