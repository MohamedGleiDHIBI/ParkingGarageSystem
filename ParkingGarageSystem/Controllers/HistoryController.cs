using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.Services;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistory _History;
        public HistoryController(IHistory history)
        {
            _History = history;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHistories()
        {
            var histories = await _History.GetAllHistories();
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var history = await _History.GetHistory(id);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> AddHistory([FromBody] History history)
        {
            await _History.AddHistory(history);
            return CreatedAtAction(nameof(GetHistory), new { id = history.Id }, history);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistory(int id, [FromBody] History history)
        {
            {
                if (id != history.Id)
                {
                    return BadRequest();
                }

                var existingHistory = await _History.GetHistory(id);
                if (existingHistory == null)
                {
                    return NotFound();
                }

                await _History.UpdateHistory(history);
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            var history = await _History.GetHistory(id);
            if (history == null)
            {
                return NotFound();
            }

            await _History.DeleteHistory(id);
            return NoContent();
        }
    }
}
