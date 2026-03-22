using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.RevogarGestor;

public record RevogarGestorCommand(Guid UsuarioId) : IRequest<Result<RevogarGestorResponse>>;

public record RevogarGestorResponse(Guid UsuarioId, string Mensagem);
