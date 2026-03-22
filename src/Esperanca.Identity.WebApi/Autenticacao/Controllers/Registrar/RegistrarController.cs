using Esperanca.Identity.Application.Autenticacao.Registrar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Autenticacao.Controllers.Registrar;

[ApiController]
[Route("api/auth")]
public class RegistrarController(IMediator mediator) : ControllerBase
{
    [HttpPost("registrar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar([FromBody] RegistrarCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return result.Sucesso
            ? StatusCode(result.StatusCode, result.Dados)
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
