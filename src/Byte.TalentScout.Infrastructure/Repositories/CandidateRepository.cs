using Byte.TalentScout.Database;
using Microsoft.EntityFrameworkCore;
using Byte.TalentScout.Domain.Entities;
using Byte.TalentScout.Domain.Abstractions.Repositories;

namespace Byte.TalentScout.Infrastructure.Repositories;

public class CandidateRepository(TalentScoutIdentityDbContext talentScoutDbContext) : ICandidateRepository
{
    public async Task<IEnumerable<Candidate>> GetAll(CancellationToken cancellationToken)
       => await talentScoutDbContext.Candidates.ToListAsync(cancellationToken);

    public async Task<Candidate> GetCandidateById(long id, CancellationToken cancellationToken)
       => await talentScoutDbContext.Candidates.FindAsync(id, cancellationToken);

    public async Task Insert(Candidate candidate, CancellationToken cancellationToken)
    {
        talentScoutDbContext.Candidates.Add(candidate);

        await talentScoutDbContext.SaveChangesAsync(cancellationToken);
    }
}
