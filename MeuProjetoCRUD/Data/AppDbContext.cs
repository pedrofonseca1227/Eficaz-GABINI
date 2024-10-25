using Microsoft.EntityFrameworkCore;
using MeuProjetoCRUD.Models;
using Microsoft.Extensions.Configuration;

namespace MeuProjetoCRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
