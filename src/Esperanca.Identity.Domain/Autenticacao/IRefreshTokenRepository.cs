namespace Esperanca.Identity.Domain.Autenticacao;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> ObterPorTokenAsync(string token, CancellationToken ct = default);
    Task AdicionarAsync(RefreshToken refreshToken, CancellationToken ct = default);
    Task AtualizarAsync(RefreshToken refreshToken, CancellationToken ct = default);
}
