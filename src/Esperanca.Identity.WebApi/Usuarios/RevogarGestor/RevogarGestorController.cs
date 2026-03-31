using Esperanca.Identity.Application.Usuarios.RevogarGestor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Usuarios.RevogarGestor;

[ApiController]
[Route("api/usuarios")]
[Authorize(Roles = "Admin")]
[Tags("Usuários")]
public class RevogarGestorController(IMediator mediator) : ControllerBase
{
    [HttpDelete("{id:guid}/revogar-gestor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RevogarGestor(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new RevogarGestorCommand(id), ct);
        return result.Sucesso
            ? Ok(result.Dados)
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
