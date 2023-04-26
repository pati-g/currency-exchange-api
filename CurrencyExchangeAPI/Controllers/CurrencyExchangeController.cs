using Microsoft.AspNetCore.Mvc;
using NBPAPIClient;

namespace Shared.Controllers
{
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly NBPClient _client;
        private readonly ILogger<CurrencyExchangeController> _logger;

        public CurrencyExchangeController(ILogger<CurrencyExchangeController> logger, NBPClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet("/exchange-rate")]
        [ProducesResponseType(typeof(double), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAverageExchangeRateAsync([FromQuery] CurrencyRequestDto request)
        {
            if (request == null)
            {
                _logger.LogWarning("Null argument of type {arg}", typeof(CurrencyRequestDto));
                return BadRequest("Please provide the currency code and date");
            }
            try
            {
                var result = await _client.GetAverageExchangeRateAsync(request.Date.ToString(), request.CurrencyCode);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound("The requested currency code or date are incorrect, please try again. Make sure to use the correct date format: YYY-MM-DD");
            }
        }

        [HttpGet("/max-min-rate")]
        [ProducesResponseType(typeof(MinMaxRateResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMaxAndMinExchangeRateAsync([FromQuery] CurrencyQuotationsRequestDto request)
        {
            if (request == null)
            {
                _logger.LogWarning("Null argument of type {arg}", typeof(CurrencyRequestDto));
                return BadRequest("Please provide the currency code and date");
            }

            try
            {
                var result = await _client.GetMaxAndMinExchangeRateAsync(request.CurrencyCode, request.LastQuotations);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound("The requested currency code could not be found");
            }
        }

        [HttpGet("/ask-bid-difference")]
        [ProducesResponseType(typeof(double), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAskBidMajorDifferenceAsync([FromQuery] CurrencyQuotationsRequestDto request)
        {
            if (request == null)
            {
                _logger.LogWarning("Null argument of type {arg}", typeof(CurrencyRequestDto));
                return BadRequest("Please provide the currency code and date");
            }

            try
            {
                var result = await _client.GetAskBidMajorDifferenceAsync(request.CurrencyCode, request.LastQuotations);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound("The requested currency code could not be found");
            }
        }
    }
}