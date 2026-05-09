using Esperanca.Identity.Application._Shared.Localization;
using FluentValidation;

namespace Esperanca.Identity.Application.Usuarios.AlterarSenha;

public class AlterarSenhaValidator : AbstractValidator<AlterarSenhaCommand>
{
    public AlterarSenhaValidator(IAppLocalizer localizer)
    {
        RuleFor(x => x.SenhaAtual)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.SenhaAtualObrigatoria]);

        RuleFor(x => x.NovaSenha)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.NovaSenhaObrigatoria])
            .MinimumLength(8).WithMessage(localizer[IdentityErrorCodes.NovaSenhaMinimo8]);
    }
}
