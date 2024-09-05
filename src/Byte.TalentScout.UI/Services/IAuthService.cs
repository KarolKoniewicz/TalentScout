using Byte.TalentScout.Domain.ViewModels;

namespace Byte.TalentScout.UI.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginViewModel loginViewModel);
    Task Logout();
    Task<RegisterResult> Register(RegisterViewModel registerViewModel);
}
