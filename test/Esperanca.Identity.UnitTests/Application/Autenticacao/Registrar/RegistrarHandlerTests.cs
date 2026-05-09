using Esperanca.Identity.Application._Shared.Localization;
using Esperanca.Identity.Domain.Usuarios.Enums;
using Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Fakers;
using Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar.Fixtures;

namespace Esperanca.Identity.UnitTests.Application.Autenticacao.Registrar;

public sealed class RegistrarHandlerTests
{
    [Fact]
    public async Task Handle_DeveRegistrarUsuarioComRoleDoador_QuandoDadosValidos()
    {
        // Arrange
        var fixture = new RegistrarHandlerFixture();
        var command = RegistrarCommandFaker.Valid();
        var roleDoador = RoleFaker.Doador();

        fixture.UsuarioRepositoryMock.SetupEmailExisteAsync(command.Email, false);
        fixture.PasswordHasherMock.SetupHash(command.Senha, "hashed_password");
        fixture.RoleRepositoryMock.SetupObterPorTipoAsync(RoleTipo.Doador, roleDoador);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Sucesso);
        Assert.Equal(201, result.StatusCode);
        Assert.NotNull(result.Dados);
        Assert.Equal(command.Nome, result.Dados.Nome);
        Assert.Equal(command.Email.ToLowerInvariant(), result.Dados.Email);
        fixture.UsuarioRepositoryMock.VerifyAdicionarAsyncChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncChamado();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoEmailJaCadastrado()
    {
        // Arrange
        var fixture = new RegistrarHandlerFixture();
        var command = RegistrarCommandFaker.Valid();

        fixture.UsuarioRepositoryMock.SetupEmailExisteAsync(command.Email, true);

        var handler = fixture.CriarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Sucesso);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(IdentityErrorCodes.EmailJaCadastrado, result.Erro);
        fixture.UsuarioRepositoryMock.VerifyAdicionarAsyncNaoChamado();
        fixture.AppDbContextMock.VerifySaveChangesAsyncNaoChamado();
    }
}
