namespace Byte.TalentScout.Domain.Entities;


public class Candidate
{
    public long Id { get; set; } // would go for Guid normally.
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string CirriculumVitaePath { get; set; }
    public DateTimeOffset CreatedOn { get; } = DateTimeOffset.UtcNow;

    public ICollection<Degree> Degrees { get; set; }
}
