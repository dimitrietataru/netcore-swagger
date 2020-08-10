using Microsoft.AspNetCore.Mvc;

namespace NetCore.SwaggerPrototype.App.Controllers
{
    ////[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    public sealed class FooController : ControllerBase
    {
        [HttpGet]
        [Route("foos")]
        public IActionResult Test()
        {
            return Ok("Response from api/v1/foos");
        }
    }
}
