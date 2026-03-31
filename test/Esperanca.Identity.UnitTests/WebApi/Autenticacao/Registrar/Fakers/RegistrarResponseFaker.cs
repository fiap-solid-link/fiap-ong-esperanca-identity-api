using Esperanca.Identity.Application.Autenticacao.Registrar;

namespace Esperanca.Identity.UnitTests.WebApi.Autenticacao.Registrar.Fakers;

public static class RegistrarResponseFaker
{
    public static RegistrarResponse Valid(
        Guid? id = null,
        string nome = "João Silva",
        string email = "joao@email.com")
    {
        return new RegistrarResponse(id ?? Guid.NewGuid(), nome, email);
    }
}
