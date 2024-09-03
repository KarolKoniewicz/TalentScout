using Byte.TalentScout.Domain.Entities;

namespace Byte.TalentScout.Domain.Abstractions.Repositories;

public interface IDegreeRepository
{
    public Task<IEnumerable<Degree>> GetAll(CancellationToken cancellationToken);
    public Task<Degree> GetDegreeById(long id, CancellationToken cancellationToken);
    public Task Insert(Degree degree, CancellationToken cancellationToken);
}
