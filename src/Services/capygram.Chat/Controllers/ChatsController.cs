using capygram.Chat.Domain.Model;
using capygram.Chat.Services;
using capygram.Common.Shared;
using Microsoft.AspNetCore.Mvc;

namespace capygram.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatServices _chatServices;

        public ChatController(IChatServices chatServices)
        {
            _chatServices = chatServices;
        }

        //[HttpDelete("delete")]
        //public async Task<IActionResult> DeleteMessageById([FromBody] DeleteMessageRequest deleteMessageRequest)
        //{
        //    var result = await _chatServices.DeleteMessageById(deleteMessageRequest);
        //    return Ok(result);
        //}

        [HttpGet("messages/{userId}")]
        public async Task<IActionResult> GetMessagesByUserId(Guid userId)
        {
            var result = await _chatServices.GetMessagesByUserId(userId);
            return Ok(result);
        }
    }
}
