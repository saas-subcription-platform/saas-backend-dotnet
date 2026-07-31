using Microsoft.AspNetCore.Mvc;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Controllers
{
    [ApiController]
    [Route("api/conversations")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly ISpringBootUserService _springBootUserService;

        public ConversationController(
            IConversationService conversationService,
            ISpringBootUserService springBootUserService)
        {
            _conversationService = conversationService;
            _springBootUserService = springBootUserService;
        }

        [HttpGet("direct/{userId}")]
        public async Task<IActionResult> GetOrCreateDirectConversation(long userId)
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) ||
                !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("JWT Token is missing.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            // Logged-in user from Spring Boot
            var currentUser = await _springBootUserService.GetCurrentUserAsync(token);

            var conversation = await _conversationService
                .GetOrCreateDirectConversationAsync(
                    currentUser.UserId,
                    userId);

            return Ok(conversation);
        }
    }
}