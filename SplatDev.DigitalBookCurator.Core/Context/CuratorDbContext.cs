using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using SplatDev.DigitalBookCurator.Core.Models;

namespace SplatDev.DigitalBookCurator.Core.Context
{
    public class CuratorDbContextFactory : IDesignTimeDbContextFactory<CuratorDbContext>
    {
        public CuratorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CuratorDbContext>();
            optionsBuilder.UseSqlite("Data Source=.\\Database\\Curator.db");

            return new CuratorDbContext(optionsBuilder.Options);
        }
    }
    public class CuratorDbContext : DbContext
    {
        public CuratorDbContext(DbContextOptions<CuratorDbContext> options) : base(options) { }
        public CuratorDbContext() { }
        public virtual DbSet<Book> Books { get; set; } = default!;
    }
}
