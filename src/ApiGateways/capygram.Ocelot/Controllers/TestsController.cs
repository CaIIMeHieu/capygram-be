using capygram.Auth.Domain.Repositories;
using capygram.Auth.Services;
using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.User;
using capygram.Common.Filters;
using capygram.Common.Identity;
using capygram.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace capygram.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IExternalService _externalService;
        public TestsController(IExternalService externalService)
        {
            _externalService = externalService;
        }
        [HttpGet("RequestPostApi")]
        public async Task<IActionResult> RequestPostApi()
        {
            var result = await _externalService.GetExternalDataListAsync<List<PostDBDTO>>("capygram-post:8081/api/Posts/GetAll?pageSize=10&pageNumber=1");
            return Ok(result);
        }
    }
}
