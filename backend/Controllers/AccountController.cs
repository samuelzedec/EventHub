using backend.Extensions;
using backend.Models;
using backend.Services;
using backend.ViewModels;
using backend.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
namespace backend.Controllers;

[ApiController]
[Route("/account")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    public AccountController(AccountService service)
        => _service = service;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAccount(
        [FromBody] EditorAccountViewModel data) 
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

        var (code, model) = await _service.CreateAccountAsync(data);
        return code switch
        {
            201 => Created($"/account/register/{model.Data?.Id}", model),
            400 => BadRequest(model),
            409 => Conflict(model),
            _ => StatusCode(500, model)
        };
    }

    [HttpPost("validation-email")]
    public async Task<IActionResult> ValidateEmail(
        [FromQuery] string email,
        [FromQuery] int code)
    {
        var (statusCode, message) = await _service.ValidatingTheEmailCode(email, code);
        return statusCode switch
        {
            200 => Ok(message),
            404 => BadRequest(message),
            422 => UnprocessableEntity(message),
            _ => StatusCode(500, new ResultViewModel<string>(["Erro interno no servidor"]))
        };
    }


    // [HttpPost("login")]
    // public async Task<IActionResult> LoginAsync() 
    // {

    //     return Ok();
    // }        
}