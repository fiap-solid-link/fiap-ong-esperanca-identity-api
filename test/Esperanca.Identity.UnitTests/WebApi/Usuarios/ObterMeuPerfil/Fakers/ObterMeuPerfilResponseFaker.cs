using Esperanca.Identity.Application.Usuarios.ObterMeuPerfil;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.ObterMeuPerfil.Fakers;

public static class ObterMeuPerfilResponseFaker
{
    public static ObterMeuPerfilResponse Valid(
        Guid? id = null,
        string nome = "João Silva",
        string email = "joao@email.com",
        string? apelido = "joao",
        List<string>? roles = null)
    {
        return new ObterMeuPerfilResponse(
            id ?? Guid.NewGuid(),
            nome,
            email,
            apelido,
            roles ?? new List<string> { "Doador" });
    }
}
