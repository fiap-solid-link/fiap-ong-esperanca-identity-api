using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.Application.Usuarios.AlterarSenha;

public record AlterarSenhaCommand(string SenhaAtual, string NovaSenha) : IRequest<Result<object>>;
