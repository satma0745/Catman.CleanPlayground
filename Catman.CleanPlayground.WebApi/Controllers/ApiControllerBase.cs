namespace Catman.CleanPlayground.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected string AuthorizationToken =>
            Request.Headers["Authorization"];
        
        protected IActionResult Forbidden() =>
            StatusCode(403);
        
        protected IActionResult InternalServerError() =>
            StatusCode(500);
    }
}
