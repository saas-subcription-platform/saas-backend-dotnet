using GoalandPerformance.Entities;
using GoalandPerformance.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoalandPerformance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {

        private readonly IGoalService _goalService;


        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }


        // GET: api/Goal
        [HttpGet]
        public async Task<IActionResult> GetAllGoals()
        {
            var goals = await _goalService.GetAllGoalsAsync();

            return Ok(goals);
        }



        // GET: api/Goal/company/2
        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetGoalsByCompany(long companyId)
        {
            var goals = await _goalService
                .GetGoalsByCompanyIdAsync(companyId);

            return Ok(goals);
        }



        // GET: api/Goal/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoalById(long id)
        {
            var goal = await _goalService
                .GetGoalByIdAsync(id);


            if (goal == null)
            {
                return NotFound();
            }


            return Ok(goal);
        }



        // POST: api/Goal
        [HttpPost]
        public async Task<IActionResult> CreateGoal(
            [FromBody] Goal goal)
        {

            var createdGoal =
                await _goalService.CreateGoalAsync(goal);


            return Ok(createdGoal);
        }



        // PUT: api/Goal/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoal(
            long id,
            [FromBody] Goal goal)
        {

            var result =
                await _goalService.UpdateGoalAsync(id, goal);


            if (!result)
            {
                return NotFound();
            }


            return Ok(new
            {
                message = "Goal updated successfully"
            });
        }



        // DELETE: api/Goal/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(long id)
        {

            var result =
                await _goalService.DeleteGoalAsync(id);


            if (!result)
            {
                return NotFound();
            }


            return Ok(new
            {
                message = "Goal deleted successfully"
            });
        }

    }
}