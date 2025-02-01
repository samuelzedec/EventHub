using Microsoft.AspNetCore.Mvc;
namespace backend.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Get()
        => Ok();     
}