using Esperanca.Identity.Application.Usuarios.AtualizarPerfil;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Usuarios.Controllers.AtualizarPerfil;

[ApiController]
[Route("api/usuarios")]
[Authorize]
public class AtualizarPerfilController(IMediator mediator) : ControllerBase
{
    [HttpPut("perfil")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return result.Sucesso
            ? Ok(result.Dados)
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
