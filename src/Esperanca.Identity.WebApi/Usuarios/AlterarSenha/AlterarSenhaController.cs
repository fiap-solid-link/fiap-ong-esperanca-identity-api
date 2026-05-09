using Esperanca.Identity.Application.Usuarios.AlterarSenha;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esperanca.Identity.WebApi.Usuarios.AlterarSenha;

[ApiController]
[Route("api/usuarios")]
[Authorize]
[Tags("Usuários")]
public class AlterarSenhaController(IMediator mediator) : ControllerBase
{
    [HttpPatch("senha")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return result.Sucesso
            ? Ok()
            : StatusCode(result.StatusCode, new { erro = result.Erro });
    }
}
