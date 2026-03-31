using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.ObterMeuPerfil;

public record ObterMeuPerfilQuery : IRequest<Result<ObterMeuPerfilResponse>>;

public record ObterMeuPerfilResponse(Guid Id, string Nome, string Email, string? Apelido, List<string> Roles);
