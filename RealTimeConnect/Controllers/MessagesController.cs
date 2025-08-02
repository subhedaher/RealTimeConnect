using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeConnect.DTOs;
using RealTimeConnect.Hubs;
using RealTimeConnect.Interfaces;

namespace RealTimeConnect.Controllers
{
    [Route("api/messages")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IHubContext<ChatHub> _chatHub;

        public MessagesController(IMessageService messageService, IHubContext<ChatHub> chatHub)
        {
            this.messageService = messageService;
            this._chatHub = chatHub;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendMessageDto dto)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var senderUserName = User.Identity.Name;

            if (int.Parse(senderId) == dto.ReceiverId) return BadRequest("Cannot send message to yourself.");

            await messageService.SendMessageAsync(int.Parse(senderId), dto.ReceiverId, dto.Content);

            await _chatHub.Clients.User(dto.ReceiverId.ToString())
                    .SendAsync("ReceiveMessage", senderId, senderUserName, dto.Content);
            return Ok("Message sent.");
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.Parse(currentUser) == userId) return BadRequest("Cannot fetch messages with yourself.");

            var messages = await messageService.GetMessagesAsync(int.Parse(currentUser), userId);
            return Ok(messages);
        }
    }
}
