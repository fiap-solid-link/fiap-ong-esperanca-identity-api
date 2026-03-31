using Esperanca.Identity.Application.Usuarios.ConcederGestor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Usuarios.ConcederGestor;

[ApiController]
[Route("api/usuarios")]
[Authorize(Roles = "Admin")]
[Tags("Usuários")]
public class ConcederGestorController(IMediator mediator) : ControllerBase
{
    [HttpPost("{id:guid}/conceder-gestor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConcederGestor(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new ConcederGestorCommand(id), ct);
        return result.Sucesso
            ? Ok(result.Dados)
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
