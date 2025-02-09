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

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync() 
    {

        return Ok();
    }        
}