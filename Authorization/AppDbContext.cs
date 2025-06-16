using Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace Authorization;
public class AppDbContext : DbContext
{
    public DbSet<UserData> UserDatas{ get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
