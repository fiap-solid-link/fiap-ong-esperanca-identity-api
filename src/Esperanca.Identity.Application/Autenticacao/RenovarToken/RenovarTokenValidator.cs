using Esperanca.Identity.Application._Shared.Localization;
using FluentValidation;

namespace Esperanca.Identity.Application.Autenticacao.RenovarToken;

public class RenovarTokenValidator : AbstractValidator<RenovarTokenCommand>
{
    public RenovarTokenValidator(IAppLocalizer localizer)
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.RefreshTokenObrigatorio]);
    }
}
