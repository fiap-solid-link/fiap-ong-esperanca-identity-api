using Esperanca.Identity.Application.Usuarios.AtualizarPerfil;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.AtualizarPerfil.Fakers;

public static class AtualizarPerfilResponseFaker
{
    public static AtualizarPerfilResponse Valid(
        Guid? id = null,
        string nome = "João Atualizado",
        string? apelido = "joao123")
    {
        return new AtualizarPerfilResponse(id ?? Guid.NewGuid(), nome, apelido);
    }
}
