using Microsoft.AspNetCore.Mvc;
using Services;

namespace Shared.Controllers
{
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private const string NULL_CURRENCY_QUOTATIONS_ERROR_MESSAGE = "Please provide the currency code and number of last quotations";
        private const string CURRENCY_QUOTATIONS_NOTFOUND_MESSAGE = "No ask/bid rates for the requested currency code and quotation number were found";
        private readonly ICurrencyExchangeService _service;
        private readonly ILogger<CurrencyExchangeController> _logger;

        public CurrencyExchangeController(ILogger<CurrencyExchangeController> logger, ICurrencyExchangeService client)
        {
            _logger = logger;
            _service = client;
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
                var result = await _service.GetAverageExchangeRateAsync(request.Date, request.CurrencyCode);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("{msg}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning("{msg}", ex.Message);
                return NotFound("The exchange rate for the currency code and date provided could not be found.");
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
                return BadRequest(NULL_CURRENCY_QUOTATIONS_ERROR_MESSAGE);
            }

            try
            {
                var result = await _service.GetMaxAndMinExchangeRateAsync(request.CurrencyCode, request.LastQuotations);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("{msg}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning("{msg}", ex.Message);
                return NotFound(CURRENCY_QUOTATIONS_NOTFOUND_MESSAGE);
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
                return BadRequest(NULL_CURRENCY_QUOTATIONS_ERROR_MESSAGE);
            }

            try
            {
                var result = await _service.GetAskBidMajorDifferenceAsync(request.CurrencyCode, request.LastQuotations);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("{msg}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning("{msg}", ex.Message);
                return NotFound(CURRENCY_QUOTATIONS_NOTFOUND_MESSAGE);
            }
        }
    }
}