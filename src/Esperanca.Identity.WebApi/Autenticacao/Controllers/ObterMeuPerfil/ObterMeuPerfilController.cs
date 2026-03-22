using Esperanca.Identity.Application.Autenticacao.ObterMeuPerfil;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Autenticacao.Controllers.ObterMeuPerfil;

[ApiController]
[Route("api/auth")]
[Authorize]
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
