using Microsoft.EntityFrameworkCore;
using MoodService.Data;
using MoodService.Models;

namespace MoodService.Services
{
    public class MoodServiceHandler : IMoodService
    {
        private readonly MoodDbContext _context;

        public MoodServiceHandler(MoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<MoodEntry>> GetAllAsync()
        {
            return await _context.MoodEntries.ToListAsync();
        }

        public async Task AddEntryAsync(MoodEntry entry)
        {
            await _context.MoodEntries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
    }

}
