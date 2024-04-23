using Microsoft.EntityFrameworkCore;

namespace WebAtonTest.Data
{
    public class WebAtonTestDbContext : DbContext
    {
        public WebAtonTestDbContext(DbContextOptions<WebAtonTestDbContext> options)
      : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<User> Users { get; set; }
    }
}
