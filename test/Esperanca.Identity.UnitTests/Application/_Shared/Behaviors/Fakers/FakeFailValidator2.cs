using FluentValidation;

namespace Esperanca.Identity.UnitTests.Application._Shared.Behaviors.Fakers;

public sealed class FakeFailValidator2 : AbstractValidator<FakeCommand>
{
    public FakeFailValidator2()
    {
        RuleFor(x => x.Nome).MinimumLength(10).WithMessage("Nome deve ter no mínimo 10 caracteres.");
    }
}
