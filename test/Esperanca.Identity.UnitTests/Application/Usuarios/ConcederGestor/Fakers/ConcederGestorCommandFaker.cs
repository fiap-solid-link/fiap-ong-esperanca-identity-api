using Esperanca.Identity.Application.Usuarios.ConcederGestor;

namespace Esperanca.Identity.UnitTests.Application.Usuarios.ConcederGestor.Fakers;

public static class ConcederGestorCommandFaker
{
    public static ConcederGestorCommand Valid(Guid? usuarioId = null)
    {
        return new ConcederGestorCommand(usuarioId ?? Guid.NewGuid());
    }
}
