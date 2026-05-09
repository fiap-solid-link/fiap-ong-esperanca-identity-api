using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.Application.Autenticacao.Login;
using MediatR;

namespace Esperanca.Identity.Application.Autenticacao.RenovarToken;

public record RenovarTokenCommand(string RefreshToken) : IRequest<Result<LoginResponse>>;
