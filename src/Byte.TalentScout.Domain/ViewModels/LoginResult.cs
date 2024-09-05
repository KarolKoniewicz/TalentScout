namespace Byte.TalentScout.Domain.ViewModels;

public record LoginResult(bool Successful, string Error, string Token);
