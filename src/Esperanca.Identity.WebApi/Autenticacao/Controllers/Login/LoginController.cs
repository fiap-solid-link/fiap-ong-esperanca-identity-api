using Esperanca.Identity.Application.Autenticacao.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Autenticacao.Controllers.Login;

[ApiController]
[Route("api/auth")]
public class LoginController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return result.Sucesso
            ? Ok(result.Dados)
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
