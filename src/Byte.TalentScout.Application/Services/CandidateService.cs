using Byte.TalentScout.Domain.Entities;
using Byte.TalentScout.Domain.Abstractions.Services;
using Byte.TalentScout.Domain.Abstractions.Repositories;

namespace Byte.TalentScout.Application.Services;

public class CandidateService(ICandidateRepository candidateRepository) : ICandidateService
{
    private readonly ICandidateRepository candidateRepository = candidateRepository;

    //Would you DTO's and mapper in real case scenario.
    public async Task<List<Candidate>> GetAll(CancellationToken cancellationToken)
        => (await candidateRepository.GetAll(cancellationToken)).ToList();

    public async Task<Candidate> GetCandidateById(long id, CancellationToken cancellationToken)
       => await candidateRepository.GetCandidateById(id, cancellationToken);

    public async Task Insert(Candidate candidate, CancellationToken cancellationToken)
    {
        await candidateRepository.Insert(candidate, cancellationToken);
    }
}
