namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System.Net.Mime;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
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
