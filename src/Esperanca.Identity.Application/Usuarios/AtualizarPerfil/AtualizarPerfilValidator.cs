using Esperanca.Identity.Application._Shared.Localization;
using FluentValidation;

namespace Esperanca.Identity.Application.Usuarios.AtualizarPerfil;

public class AtualizarPerfilValidator : AbstractValidator<AtualizarPerfilCommand>
{
    public AtualizarPerfilValidator(IAppLocalizer localizer)
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(localizer[IdentityErrorCodes.NomeObrigatorio])
            .MaximumLength(150).WithMessage(localizer[IdentityErrorCodes.NomeMaximo150]);

        RuleFor(x => x.Apelido)
            .MaximumLength(100).WithMessage(localizer[IdentityErrorCodes.ApelidoMaximo100]);
    }
}
