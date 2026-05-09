using Esperanca.Identity.Domain.Autenticacao;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.AlterarSenha.Fakers;

public static class UsuarioFaker
{
    public static Usuario Valid(
        string nome = "Maria Silva",
        string email = "maria@email.com",
        string senhaHash = "hashed_senha_atual")
    {
        return new Usuario(nome, email, senhaHash);
    }
}
