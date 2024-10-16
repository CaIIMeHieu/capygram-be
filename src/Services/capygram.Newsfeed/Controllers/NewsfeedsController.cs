using capygram.Newsfeed.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace capygram.Newsfeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsfeedsController : ControllerBase
    {
        private readonly INewsfeedService _newsfeedService;
        public NewsfeedsController( INewsfeedService newsfeedService ) {
            _newsfeedService = newsfeedService;
        }

        [HttpGet("GetNewsfeed")]        
        public async Task<IActionResult> GetNewsfeedsByUserID( [FromQuery]Guid id , int limit) {
            var result = await _newsfeedService.GetNewsfeedsByUserId(id, limit);
            return Ok(result);
        }
    }
}
