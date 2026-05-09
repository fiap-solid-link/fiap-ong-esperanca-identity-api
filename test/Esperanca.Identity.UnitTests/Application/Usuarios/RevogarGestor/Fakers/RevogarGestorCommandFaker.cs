using Esperanca.Identity.Application.Usuarios.RevogarGestor;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.RevogarGestor.Fakers;

public static class RevogarGestorCommandFaker
{
    public static RevogarGestorCommand Valid(Guid? usuarioId = null)
    {
        return new RevogarGestorCommand(usuarioId ?? Guid.NewGuid());
    }
}
