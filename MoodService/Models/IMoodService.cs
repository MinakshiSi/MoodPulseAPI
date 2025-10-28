namespace MoodService.Models
{
    public interface IMoodService
    {
        Task<List<MoodEntry>> GetAllAsync();
        Task AddEntryAsync(MoodEntry entry);
    }
}
