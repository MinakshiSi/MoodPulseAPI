using Microsoft.EntityFrameworkCore;
using MoodService.Models;

namespace MoodService.Data
{
    public class MoodDbContext : DbContext
    {
        public MoodDbContext(DbContextOptions<MoodDbContext> options)
            : base(options) { }

        public DbSet<MoodEntry> MoodEntries { get; set; }
    }
}
