using Microsoft.AspNetCore.Mvc;

namespace NetCore.SwaggerPrototype.App.Controllers
{
    ////[Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    public sealed class Foo2Controller : ControllerBase
    {
        [HttpGet]
        [Route("foos")]
        public IActionResult Test()
        {
            return Ok("Response from api/v2/foos");
        }
    }
}
