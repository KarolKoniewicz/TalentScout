using Microsoft.AspNetCore.Mvc;
using Byte.TalentScout.Domain.Entities;
using Byte.TalentScout.Domain.Abstractions.Services;

namespace Byte.TalentScout.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        // GET: api/candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidates(CancellationToken cancellationToken)
        {
            var candidates = await candidateService.GetAll(cancellationToken);
            return Ok(candidates);
        }

        // GET: api/candidates/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidateById(long id, CancellationToken cancellationToken)
        {
            var candidate = await candidateService.GetCandidateById(id, cancellationToken);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        // POST: api/candidates/create
        [HttpPost("create")]
        public async Task<ActionResult<Candidate>> CreateCandidate(Candidate newCandidate, CancellationToken cancellationToken)
        {
            await candidateService.Insert(newCandidate, cancellationToken);
            return CreatedAtAction(nameof(GetCandidateById), new { id = newCandidate.Id }, newCandidate);
        }
    }
}
