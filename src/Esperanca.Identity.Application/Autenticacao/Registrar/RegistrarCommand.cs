using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.Application.Autenticacao.Registrar;

public record RegistrarCommand(
    string Nome,
    string Email,
    string Senha) : IRequest<Result<RegistrarResponse>>;

public record RegistrarResponse(Guid Id, string Nome, string Email);
