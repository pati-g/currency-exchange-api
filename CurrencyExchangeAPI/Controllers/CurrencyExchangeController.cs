using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CurrencyExchangeController> _logger;

        public CurrencyExchangeController(ILogger<CurrencyExchangeController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(double), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAverageExchangeRateAsync([FromQuery] CurrencyRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Please provide the currency code and date");
            }

            return Ok();
        }
    }
}