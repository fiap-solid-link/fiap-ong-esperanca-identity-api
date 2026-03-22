using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.AtualizarPerfil;

public record AtualizarPerfilCommand(
    string Nome,
    string? Apelido) : IRequest<Result<AtualizarPerfilResponse>>;

public record AtualizarPerfilResponse(Guid Id, string Nome, string? Apelido);
