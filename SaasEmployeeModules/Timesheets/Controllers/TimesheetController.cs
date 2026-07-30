using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetMyTimesheets()
        {
            // Temporary until JWT integration
            int employeeId = 25;

            var timesheets = await _timesheetService.GetAllByEmployeeIdAsync(employeeId);

            return Ok(timesheets);
        }
    }
}