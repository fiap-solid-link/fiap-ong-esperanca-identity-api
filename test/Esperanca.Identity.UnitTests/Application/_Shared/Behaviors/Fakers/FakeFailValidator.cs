using FluentValidation;

namespace Esperanca.Identity.UnitTests.Application._Shared.Behaviors.Fakers;

public sealed class FakeFailValidator : AbstractValidator<FakeCommand>
{
    public FakeFailValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório.");
    }
}