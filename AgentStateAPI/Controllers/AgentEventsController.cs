using Microsoft.AspNetCore.Mvc;
using AgentStateAPI.Models;
using AgentStateAPI.Services;

namespace AgentStateAPI.Controllers
{
    [ApiController]
    [Route("api/agent-events")]
    public class AgentEventsController : ControllerBase
    {
        private readonly AgentStateService _service;

        public AgentEventsController(AgentStateService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] AgentEvent evt)
        {
            await _service.ProcessEventAsync(evt);
            return Ok();
        }
    }
}