using Esperanca.Identity.Application._Shared.Localization;
using FluentValidation;

namespace Esperanca.Identity.Application.Autenticacao.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator(IAppLocalizer localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.EmailObrigatorio])
            .EmailAddress().WithMessage(localizer[IdentityErrorCodes.EmailInvalido]);

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.SenhaObrigatoria]);
    }
}
