namespace Esperanca.Identity.Domain.Autenticacao;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;
    public DateTime CriadoEm { get; private set; }
    public DateTime ExpiraEm { get; private set; }
    public DateTime? RevogadoEm { get; private set; }
    public bool Ativo => RevogadoEm is null && DateTime.UtcNow < ExpiraEm;

    private RefreshToken() { }

    public RefreshToken(Guid usuarioId, string token, TimeSpan validade)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        Token = token;
        CriadoEm = DateTime.UtcNow;
        ExpiraEm = CriadoEm.Add(validade);
    }

    public void Revogar()
    {
        RevogadoEm = DateTime.UtcNow;
    }
}
