using Microsoft.AspNetCore.Mvc;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ISpringBootUserService _springBootUserService;
        private readonly ITeamMemberService _teamMemberService;

        public TeamController(
        ITeamService teamService,
        ISpringBootUserService springBootUserService,
        ITeamMemberService teamMemberService)
        {
            _teamService = teamService;
            _springBootUserService = springBootUserService;
            _teamMemberService = teamMemberService;
        }

        [HttpPost("general")]
        public async Task<IActionResult> GetOrCreateGeneralTeam()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) ||
                !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("JWT Token is missing.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            // Get logged-in user from Spring Boot
            var currentUser = await _springBootUserService.GetCurrentUserAsync(token);

            // Get all users of the company
            var companyUsers = await _springBootUserService.GetCompanyUsersAsync(token);

            // Create/Get General Team and sync members
            var team = await _teamService.GetOrCreateGeneralTeamAsync(
                currentUser.CompanyId,
                currentUser.UserId,
                companyUsers);

            return Ok(team);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) ||
                !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("JWT Token is missing.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            // Get logged-in user
            var currentUser = await _springBootUserService.GetCurrentUserAsync(token);

            // Get all teams of the company
            var teams = await _teamService.GetTeamsByCompanyAsync(currentUser.CompanyId);

            return Ok(teams);
        }

        [HttpGet("{teamId}/members")]
        public async Task<IActionResult> GetTeamMembers(long teamId)
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) ||
                !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("JWT Token is missing.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            var members = await _teamMemberService.GetTeamMembersAsync(
                teamId,
                token);

            return Ok(members);
        }

        [HttpGet("company-users")]
        public async Task<IActionResult> GetCompanyUsers()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) ||
                !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("JWT Token is missing.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            var users = await _springBootUserService.GetCompanyUsersAsync(token);

            return Ok(users);
        }
    }
}