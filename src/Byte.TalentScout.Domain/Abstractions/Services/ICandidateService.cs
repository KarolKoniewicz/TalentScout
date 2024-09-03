using Byte.TalentScout.Domain.Entities;

namespace Byte.TalentScout.Domain.Abstractions.Services;

public interface ICandidateService
{
    public Task<List<Candidate>> GetAll(CancellationToken cancellationToken);
    public Task<Candidate> GetCandidateById(long id, CancellationToken cancellationToken);
    public Task Insert(Candidate candidate, CancellationToken cancellationToken);
}
