using Microsoft.AspNetCore.Mvc;
using Services.MortgagePricing.Messaging.Subscriber;

namespace Services.MortgagePricing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeadLetterController : ControllerBase
    {
        private readonly DeadLetterHandler _deadLetterHandler;

        public DeadLetterController(DeadLetterHandler deadLetterHandler)
        {
            _deadLetterHandler = deadLetterHandler;
        }

        [HttpGet("process")]
        public async Task<IActionResult> ProcessDeadLetters()
        {
            await _deadLetterHandler.ProcessDeadLetterMessagesAsync();
            return Ok("Dead letters processed.");
        }
    }
}