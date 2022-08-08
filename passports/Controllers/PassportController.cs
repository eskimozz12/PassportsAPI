using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using passports.Services.PassportService;



namespace passports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly IPassportService _passportService;
        public PassportController(IPassportService passportService)
        {
            _passportService = passportService;
        }


        [HttpGet("{series}/{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Passports>> GetInactivePassportAsync(int series, int number)
        {

            var session = await _passportService.GetInactivePassportAsync(series, number);
            if (session == null)
            {
                return NotFound();
            }
            else return Ok(session);
        }

        [HttpGet("{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<HistoryItem>> GetHistoryAsync(DateTime date)
        {
            return await _passportService.GetHistoryAsync(date);
        }

        [HttpGet("history{series}/{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<HistoryItem>> GetHistoryAsync(int series, int number)
        {
            return await _passportService.GetHistoryAsync(series, number);
        }

    }
}
