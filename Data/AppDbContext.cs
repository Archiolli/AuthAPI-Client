using AppClients.Models;
using Microsoft.EntityFrameworkCore;

namespace AppClients.Data 
{    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
				
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}