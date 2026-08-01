using Microsoft.AspNetCore.Mvc;
using Timesheets.DTOs.Requests;
using Timesheets.Services.Interfaces;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimesheetsController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetsController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTimesheets([FromQuery] int employeeId)
        {
            var result = await _timesheetService.GetAllByEmployeeIdAsync(employeeId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimesheetById(int id, [FromQuery] int employeeId)
        {
            var result = await _timesheetService.GetByIdAsync(id, employeeId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimesheet([FromBody] TimesheetRequest request, [FromQuery] int employeeId)
        {
            Console.WriteLine("POST HIT");

            var result = await _timesheetService.CreateAsync(request, employeeId);

            return CreatedAtAction(
                nameof(GetTimesheetById),
                new { id = result.Id, employeeId },
                result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimesheet(int id, [FromBody] TimesheetRequest request, [FromQuery] int employeeId)
        {
            var result = await _timesheetService.UpdateAsync(
                id,
                request,
                employeeId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPatch("{id}/submit")]
        public async Task<IActionResult> SubmitTimesheet(int id, [FromQuery] int employeeId)
        {
            var result = await _timesheetService.SubmitAsync(id, employeeId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}