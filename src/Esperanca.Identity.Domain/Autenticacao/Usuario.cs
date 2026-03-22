using Esperanca.Identity.Domain.Usuarios.Enums;

namespace Esperanca.Identity.Domain.Autenticacao;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty;
    public string? Apelido { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime? AtualizadoEm { get; private set; }

    private readonly List<Role> _roles = [];
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    private readonly List<RefreshToken> _refreshTokens = [];
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private Usuario() { }

    public Usuario(string nome, string email, string senhaHash)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email.ToLowerInvariant();
        SenhaHash = senhaHash;
        CriadoEm = DateTime.UtcNow;
    }

    public void AtualizarPerfil(string nome, string? apelido)
    {
        Nome = nome;
        Apelido = apelido;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AdicionarRole(Role role)
    {
        if (_roles.Any(r => r.Tipo == role.Tipo))
            return;

        _roles.Add(role);
        AtualizadoEm = DateTime.UtcNow;
    }

    public void RemoverRole(RoleTipo tipo)
    {
        var role = _roles.FirstOrDefault(r => r.Tipo == tipo);
        if (role is not null)
        {
            _roles.Remove(role);
            AtualizadoEm = DateTime.UtcNow;
        }
    }

    public bool PossuiRole(RoleTipo tipo) => _roles.Any(r => r.Tipo == tipo);

    public void AdicionarRefreshToken(RefreshToken refreshToken)
    {
        _refreshTokens.Add(refreshToken);
    }

    public void RevogarTodosRefreshTokens()
    {
        foreach (var token in _refreshTokens.Where(t => t.Ativo))
            token.Revogar();
    }
}
