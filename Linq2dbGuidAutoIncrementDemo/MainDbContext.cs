using Microsoft.EntityFrameworkCore;

namespace Linq2dbGuidAutoIncrementDemo
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
