using Byte.TalentScout.Domain.Entities;

namespace Byte.TalentScout.Domain.Abstractions.Services;

public interface IDegreeService
{
    public Task<List<Degree>> GetAll(CancellationToken cancellationToken);
    public Task<Degree> GetDegreeById(long id, CancellationToken cancellationToken);
    public Task Insert(Degree degree, CancellationToken cancellationToken);
}
