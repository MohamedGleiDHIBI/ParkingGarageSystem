using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.Services;
using ParkingGarageSystem.ViewModels.History;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistory _History;
        private readonly IMapper _Mapper;
        public HistoryController(IHistory history, IMapper mapper)
        {
            _History = history;
            _Mapper = mapper;
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
        public async Task<IActionResult> AddHistory([FromBody] HistoryModelView historyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var history = _Mapper.Map<History>(historyModel);
            await _History.AddHistory(history);
            return CreatedAtAction(nameof(GetHistory), new { id = history.Id }, history);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistory(int id, [FromBody] HistoryModelView historyModel)
        {
            {
                if (id != historyModel.Id)
                {
                    return BadRequest();
                }
                var historyId = await _History.GetHistory(id);

                if (historyId == null)
                    return BadRequest(new { message = "Invalid history ID. The entered ID does not match any existing history. Please double check and try again" });

                var history = _Mapper.Map<History>(historyModel);
                history.Id = id;
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
