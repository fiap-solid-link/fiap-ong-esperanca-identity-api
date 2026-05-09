using Esperanca.Identity.Application._Shared.Localization;
using FluentValidation;

namespace Esperanca.Identity.Application.Autenticacao.Registrar;

public class RegistrarValidator : AbstractValidator<RegistrarCommand>
{
    public RegistrarValidator(IAppLocalizer localizer)
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.NomeObrigatorio])
            .MaximumLength(150).WithMessage(localizer[IdentityErrorCodes.NomeMaximo150]);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.EmailObrigatorio])
            .EmailAddress().WithMessage(localizer[IdentityErrorCodes.EmailInvalido]);

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.SenhaObrigatoria])
            .MinimumLength(8).WithMessage(localizer[IdentityErrorCodes.SenhaMinimo8]);
    }
}
