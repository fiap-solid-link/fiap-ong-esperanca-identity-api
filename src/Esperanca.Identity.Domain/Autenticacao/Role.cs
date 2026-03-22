using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.Domain.Autenticacao;

public class Role
{
    public Guid Id { get; private set; }
    public RoleTipo Tipo { get; private set; }
    public string Nome { get; private set; } = string.Empty;

    private readonly List<Usuario> _usuarios = [];
    public IReadOnlyCollection<Usuario> Usuarios => _usuarios.AsReadOnly();

    private Role() { }

    public Role(RoleTipo tipo)
    {
        Id = Guid.NewGuid();
        Tipo = tipo;
        Nome = tipo.ToString();
    }
}
