﻿namespace Byte.TalentScout.Domain.ViewModels;

public class RegisterResult
{
    public bool Successful { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
