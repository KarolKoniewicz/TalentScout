using Byte.TalentScout.Domain.Enums;

namespace Byte.TalentScout.Domain.Entities;

public class Degree
{
    public long Id { get; set; } // would go for Guid normally.
    public long CandidateId { get; set; }
    public string Name { get; set; }
    public DegreeType Type { get; set; }
    public DateTimeOffset CreatedOn { get; } = DateTimeOffset.UtcNow;

    public Candidate Candidate { get; set; }
}
