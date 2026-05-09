using Esperanca.Identity.Application._Shared.Behaviors;
using Esperanca.Identity.Application._Shared.Results;
using Esperanca.Identity.UnitTests.Application._Shared.Behaviors.Fakers;
using FluentValidation;
using MediatR;

namespace Esperanca.Identity.UnitTests.Application._Shared.Behaviors;

public sealed class ValidationBehaviorTests
{
    private static RequestHandlerDelegate<Result<string>> CriarNext(Result<string> resultado, Action? callback = null)
    {
        return _ =>
        {
            callback?.Invoke();
            return Task.FromResult(resultado);
        };
    }

    [Fact]
    public async Task Handle_DeveChamarNext_QuandoNaoHaValidadores()
    {
        // Arrange
        var validators     = Enumerable.Empty<IValidator<FakeCommand>>();
        var behavior       = new ValidationBehavior<FakeCommand, Result<string>>(validators);
        var command        = new FakeCommand("Teste");
        var expectedResult = Result<string>.Ok("sucesso");
        var nextChamado = false;
        var next = CriarNext(expectedResult, () => nextChamado = true);

        // Act
        var result = await behavior.Handle(command, next, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        Assert.True(nextChamado);
    }

    [Fact]
    public async Task Handle_DeveChamarNext_QuandoValidacaoPassar()
    {
        // Arrange
        var behavior       = new ValidationBehavior<FakeCommand, Result<string>>(new[] { new FakePassValidator() });
        var command        = new FakeCommand("Teste");
        var expectedResult = Result<string>.Ok("sucesso");
        var nextChamado    = false;
        var next = CriarNext(expectedResult, () => nextChamado = true);

        // Act
        var result = await behavior.Handle(command, next, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResult, result);
        Assert.True(nextChamado);
    }

    [Fact]
    public async Task Handle_DeveLancarValidationException_QuandoValidacaoFalhar()
    {
        // Arrange
        var behavior    = new ValidationBehavior<FakeCommand, Result<string>>(new[] { new FakeFailValidator() });
        var command     = new FakeCommand("");
        var nextChamado = false;
        var next        = CriarNext(Result<string>.Ok("não deveria chegar aqui"), () => nextChamado = true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => behavior.Handle(command, next, CancellationToken.None));

        Assert.Single(exception.Errors);
        Assert.Equal("Nome", exception.Errors.First().PropertyName);
        Assert.False(nextChamado);
    }

    [Fact]
    public async Task Handle_DeveAgruparErros_QuandoMultiplosValidadoresFalham()
    {
        // Arrange
        IValidator<FakeCommand>[] validators = [new FakeFailValidator(), new FakeFailValidator2()];
        var behavior                         = new ValidationBehavior<FakeCommand, Result<string>>(validators);
        var command                          = new FakeCommand("");
        var nextChamado                      = false;
        var next = CriarNext(Result<string>.Ok("não deveria chegar aqui"), () => nextChamado = true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => behavior.Handle(command, next, CancellationToken.None));

        Assert.True(exception.Errors.Count() > 1);
        Assert.Contains(exception.Errors, e => e.PropertyName == "Nome");
        Assert.False(nextChamado);
    }
}
