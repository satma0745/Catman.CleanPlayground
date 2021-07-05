namespace Catman.CleanPlayground.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Forbidden() =>
            StatusCode(403);
        
        protected IActionResult InternalServerError() =>
            StatusCode(500);
    }
}
