using System;
using Microsoft.AspNetCore.Mvc;
using MortgagePricingService.Messaging.Publisher;
using MortgagePricingService.Services;
using Services.MortgagePricing.Models;

namespace MortgagePricingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricingController : ControllerBase
    {
        private readonly PricingPublisher _publisher;
        private readonly PricingCalculatorService _calculator;

        public PricingController(PricingPublisher publisher, PricingCalculatorService calculator)
        {
            _publisher = publisher;
            _calculator = calculator;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculatePricing([FromBody] PricingUpdate update, [FromQuery] DateTimeOffset scheduleTime)
        {
            var calculatedUpdate = _calculator.CalculatePricing(update);
            await _publisher.PublishPricingUpdateAsync(calculatedUpdate, scheduleTime);
            return Ok("Pricing update published.");
        }
    }
}

