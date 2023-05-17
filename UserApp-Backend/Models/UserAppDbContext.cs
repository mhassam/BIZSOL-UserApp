using Microsoft.EntityFrameworkCore;

namespace UserApp_Backend.Models
{
    public class UserAppDbContext : DbContext
    {
        public UserAppDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
