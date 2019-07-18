using Microsoft.EntityFrameworkCore;
using Auth.API.Models;

namespace Auth.API.Presistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }
        public DbSet<User> Users { get; set; }
    }
}


