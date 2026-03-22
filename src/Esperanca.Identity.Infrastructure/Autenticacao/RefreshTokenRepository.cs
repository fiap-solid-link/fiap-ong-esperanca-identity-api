using Esperanca.Identity.Domain.Autenticacao;
using Esperanca.Identity.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;

namespace Esperanca.Identity.Infrastructure.Autenticacao;

public class RefreshTokenRepository(IdentityDbContext context) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> ObterPorTokenAsync(string token, CancellationToken ct = default)
        => await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token, ct);

    public async Task AdicionarAsync(RefreshToken refreshToken, CancellationToken ct = default)
        => await context.RefreshTokens.AddAsync(refreshToken, ct);

    public Task AtualizarAsync(RefreshToken refreshToken, CancellationToken ct = default)
    {
        context.RefreshTokens.Update(refreshToken);
        return Task.CompletedTask;
    }
}
