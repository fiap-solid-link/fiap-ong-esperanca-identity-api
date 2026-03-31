using Esperanca.Identity.Application.Usuarios.ObterMeuPerfil;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Usuarios.ObterMeuPerfil;

[ApiController]
[Route("api/auth")]
[Authorize]
[Tags("Usuários")]
public class ObterMeuPerfilController(IMediator mediator) : ControllerBase
{
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObterMeuPerfil(CancellationToken ct)
    {
        var result = await mediator.Send(new ObterMeuPerfilQuery(), ct);
        return result.Sucesso
            ? Ok(result.Dados)
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
