using Esperanca.Identity.Application.Autenticacao.RenovarToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Autenticacao.Controllers.RenovarToken;

[ApiController]
[Route("api/auth")]
public class RenovarTokenController(IMediator mediator) : ControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RenovarToken([FromBody] RenovarTokenCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return result.Sucesso
            ? Ok(result.Dados)
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
