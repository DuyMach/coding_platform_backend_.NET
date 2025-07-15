using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Problem;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/problem")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProblemController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var problems = await _context.Problems.ToListAsync();

            var problemDetailsDtos = problems.Select(p => p.ToProblemDetailsDto());

            return Ok(problemDetailsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var problem = await _context.Problems.FindAsync(id);

            if (problem == null)
            {
                return NotFound();
            }

            return Ok(problem.ToProblemDetailsDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProblemRequestDto createProblemRequestDto)
        { 
            if (createProblemRequestDto == null)
            {
                return BadRequest("Problem data is required.");
            }

            var problemModel = createProblemRequestDto.ToProblemFromCreateDto();
            await _context.Problems.AddAsync(problemModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = problemModel.Id }, problemModel.ToProblemDetailsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProblemRequestDto updateProblemRequestDto)
        {
            var problemModel = await _context.Problems.FirstOrDefaultAsync(p => p.Id == id);

            if (problemModel == null)
            {
                return NotFound();
            }

            problemModel.Title = updateProblemRequestDto.Title;
            problemModel.Description = updateProblemRequestDto.Description;
            problemModel.Difficulty = updateProblemRequestDto.Difficulty;
            problemModel.Visibility = updateProblemRequestDto.Visibility;
            problemModel.UpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(problemModel.ToProblemDetailsDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var problemModel = await _context.Problems.FirstOrDefaultAsync(p => p.Id == id);

            if (problemModel == null)
            { 
                return NotFound();
            }

            // No RemoveAsync in DbSet definition, using Remove instead
            _context.Problems.Remove(problemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}