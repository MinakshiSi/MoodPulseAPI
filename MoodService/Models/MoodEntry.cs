using System.ComponentModel.DataAnnotations.Schema;

namespace MoodService.Models
{
    public class MoodEntry
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Emotion { get; set; }  // e.g., "Calm", "Anxious"
        [Column("MoodTrigger")]
        public string Trigger { get; set; }  // e.g., "Meeting", "Walk"
        public string RitualUsed { get; set; }  // e.g., "Balcony tea", "Scooty ride"
    }

}
