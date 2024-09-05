using Microsoft.AspNetCore.Mvc;
using Byte.TalentScout.Domain.Entities;
using Byte.TalentScout.Domain.Abstractions.Services;

namespace Byte.TalentScout.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DegreesController : ControllerBase
    {
        private readonly IDegreeService degreeService;

        public DegreesController(IDegreeService degreeService)
        {
            this.degreeService = degreeService;
        }

        // GET: api/degrees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Degree>>> GetAllDegrees(CancellationToken cancellationToken)
        {
            var degrees = await degreeService.GetAll(cancellationToken);
            return Ok(degrees);
        }

        // GET: api/degrees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Degree>> GetDegreeById(long id, CancellationToken cancellationToken)
        {
            var degree = await degreeService.GetDegreeById(id, cancellationToken);
            if (degree == null)
            {
                return NotFound();
            }
            return Ok(degree);
        }

        // POST: api/degrees/create
        [HttpPost("create")]
        public async Task<ActionResult<Degree>> CreateDegree(Degree newDegree, CancellationToken cancellationToken)
        {
            await degreeService.Insert(newDegree, cancellationToken);
            return CreatedAtAction(nameof(GetDegreeById), new { id = newDegree.Id }, newDegree);
        }

    }
}
