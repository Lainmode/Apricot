using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace Apricot.Database
{
    public class ApricotContext : DbContext
    {
        public const string connectionString = "Server=localhost\\SQLEXPRESS;Database=Apricot;Trusted_Connection=True;TrustServerCertificate=True;";
        public const string ConnectionStringLive = @"Server=mssql1.nuttyabouthosting.co.uk,56978;Database=s1765_ariagame;User ID=s1765_dilmun_user;Password=rZMF62*TOg4l;TrustServerCertificate=True;";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStringLive);
        }




        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Space> Spaces { get; set; }
        public virtual DbSet<SpaceUser> SpaceUsers { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<TextChannel> TextChannels { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

    }
}
