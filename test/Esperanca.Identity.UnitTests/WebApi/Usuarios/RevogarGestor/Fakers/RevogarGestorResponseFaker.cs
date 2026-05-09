using Esperanca.Identity.Application.Usuarios.RevogarGestor;

namespace Esperanca.Identity.UnitTests.WebApi.Usuarios.RevogarGestor.Fakers;

public static class RevogarGestorResponseFaker
{
    public static RevogarGestorResponse Valid(
        Guid? usuarioId = null,
        string mensagem = "Role revogada com sucesso")
    {
        return new RevogarGestorResponse(usuarioId ?? Guid.NewGuid(), mensagem);
    }
}
