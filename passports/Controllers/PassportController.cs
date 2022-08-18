using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using passports.Services.PassportService;
using PassportsAPI.EfCore;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InactivePassports>> GetInactivePassportAsync(int series, int number)
        {
            var session = await _passportService.GetInactivePassportAsync(series, number);
            if (session == null)
            {
                return NotFound();
            }
            else return Ok(session);
        }

        [HttpGet("history/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PassportsInfo>>> GetHistoryAsync(DateTime date)
        {
            var session = await _passportService.GetHistoryAsync(date);
            if (session.Count == 0)
            {
                return NotFound();
            }
            else return Ok(session);
        }

        [HttpGet("history/{series}/{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PassportsInfo>>> GetHistoryAsync(int series, int number)
        {
            var session = await _passportService.GetHistoryAsync(series, number);
            if (session.Count == 0)
            {
                return NotFound();
            }
            else return Ok(session);
        }

    }
}
