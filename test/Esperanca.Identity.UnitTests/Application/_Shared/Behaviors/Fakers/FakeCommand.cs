using Esperanca.Identity.Application._Shared.Results;
using MediatR;

namespace Esperanca.Identity.UnitTests.Application._Shared.Behaviors.Fakers;

public record FakeCommand(string Nome) : IRequest<Result<string>>;