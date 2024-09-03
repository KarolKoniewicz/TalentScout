using Byte.TalentScout.Domain.Entities;
using Byte.TalentScout.Domain.Abstractions.Services;
using Byte.TalentScout.Domain.Abstractions.Repositories;

namespace Byte.TalentScout.Application.Services;

public class DegreeService(IDegreeRepository degreeRepository) : IDegreeService
{
    private readonly IDegreeRepository degreeRepository = degreeRepository;

    //Would you DTO's and mapper in real case scenario.
    public async Task<List<Degree>> GetAll(CancellationToken cancellationToken)
     => (await degreeRepository.GetAll(cancellationToken)).ToList();

    public async Task<Degree> GetDegreeById(long id, CancellationToken cancellationToken)
        => await degreeRepository.GetDegreeById(id, cancellationToken);

    public async Task Insert(Degree degree, CancellationToken cancellationToken)
    {
        await degreeRepository.Insert(degree, cancellationToken);
    }
}
