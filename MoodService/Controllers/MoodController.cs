using Microsoft.AspNetCore.Mvc;
using MoodService.Models;
using MoodService.Services;

namespace MoodService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodController : ControllerBase
    {
        private readonly IMoodService _moodService;

        public MoodController(IMoodService moodService)
        {
            _moodService = moodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var moods = await _moodService.GetAllAsync();
            return Ok(moods);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MoodEntry entry)
        {
            await _moodService.AddEntryAsync(entry);
            return CreatedAtAction(nameof(GetAll), new { id = entry.Id }, entry);
        }
    }
}
