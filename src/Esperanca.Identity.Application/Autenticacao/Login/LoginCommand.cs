using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.Application.Autenticacao.Login;

public record LoginCommand(
    string Email,
    string Senha) : IRequest<Result<LoginResponse>>;

public record LoginResponse(string AccessToken, string RefreshToken, DateTime ExpiraEm);
