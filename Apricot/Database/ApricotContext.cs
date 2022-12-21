using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace Apricot.Database
{
    public class ApricotContext : DbContext
    {
        public const string connectionString = "Server=localhost\\SQLEXPRESS;Database=Apricot;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
