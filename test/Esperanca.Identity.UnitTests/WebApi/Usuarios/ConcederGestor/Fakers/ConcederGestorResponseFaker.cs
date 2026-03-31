using Esperanca.Identity.Application.Usuarios.ConcederGestor;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.ConcederGestor.Fakers;

public static class ConcederGestorResponseFaker
{
    public static ConcederGestorResponse Valid(
        Guid? usuarioId = null,
        string mensagem = "Role concedida com sucesso")
    {
        return new ConcederGestorResponse(usuarioId ?? Guid.NewGuid(), mensagem);
    }
}
