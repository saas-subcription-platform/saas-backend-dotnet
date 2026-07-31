using Microsoft.AspNetCore.Mvc;
using TeamCollaboration.DTOs.Requests;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ISpringBootUserService _springBootUserService;

        public MessageController(
            IMessageService messageService,
            ISpringBootUserService springBootUserService)
        {
            _messageService = messageService;
            _springBootUserService = springBootUserService;
        }

        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetMessages(long conversationId)
        {
            var messages = await _messageService.GetMessagesAsync(conversationId);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(
            [FromBody] SendMessageRequestDto request)
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) ||
                !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("JWT Token is missing.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            var currentUser = await _springBootUserService
                .GetCurrentUserAsync(token);

            var message = await _messageService.SendMessageAsync(
                currentUser.UserId,
                request);

            return Ok(message);
        }
    }
}