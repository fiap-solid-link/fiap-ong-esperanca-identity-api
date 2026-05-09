namespace Esperanca.Identity.Application._Shared.Localization;

public interface IAppLocalizer
{
    string this[string code] { get; }
}
