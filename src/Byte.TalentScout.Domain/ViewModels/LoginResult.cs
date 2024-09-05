using Byte.TalentScout.Domain.Extensions;

namespace Byte.TalentScout.Domain.ViewModels;

public class LoginResult
{
    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }

    public bool IsSuccessful()
        => !AccessToken.IsNullOrEmpty();
}
