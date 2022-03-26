using Announcements.Resource.Domain.Entities.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Announcements.Resource.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<AnnouncementEntity> Announcements { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
