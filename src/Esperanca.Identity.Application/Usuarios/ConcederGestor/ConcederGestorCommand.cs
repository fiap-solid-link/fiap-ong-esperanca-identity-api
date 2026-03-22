using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.ConcederGestor;

public record ConcederGestorCommand(Guid UsuarioId) : IRequest<Result<ConcederGestorResponse>>;

public record ConcederGestorResponse(Guid UsuarioId, string Mensagem);
