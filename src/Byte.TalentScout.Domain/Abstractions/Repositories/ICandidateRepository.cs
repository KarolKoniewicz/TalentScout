using Byte.TalentScout.Domain.Entities;

namespace Byte.TalentScout.Domain.Abstractions.Repositories;

public interface ICandidateRepository
{
    public Task<IEnumerable<Candidate>> GetAll(CancellationToken cancellationToken);
    public Task<Candidate> GetCandidateById(long id, CancellationToken cancellationToken);
    public Task Insert(Candidate candidate, CancellationToken cancellationToken);
}