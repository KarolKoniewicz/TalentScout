using Byte.TalentScout.Database;
using Microsoft.EntityFrameworkCore;
using Byte.TalentScout.Domain.Entities;
using Byte.TalentScout.Domain.Abstractions.Repositories;

namespace Byte.TalentScout.Infrastructure.Repositories;

public class DegreeRepository(TalentScoutIdentityDbContext talentScoutDbContext) : IDegreeRepository
{
    public async Task<IEnumerable<Degree>> GetAll(CancellationToken cancellationToken)
        => await talentScoutDbContext.Degrees.ToListAsync(cancellationToken);

    public async Task<Degree> GetDegreeById(long id, CancellationToken cancellationToken)
        => await talentScoutDbContext.Degrees.FindAsync(id, cancellationToken);

    public async Task Insert(Degree degree, CancellationToken cancellationToken)
    {
        talentScoutDbContext.Degrees.Add(degree);

        await talentScoutDbContext.SaveChangesAsync(cancellationToken);
    }
}
