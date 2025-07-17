using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Problem;
using api.Interfaces;
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
        private readonly IProblemRepository _problemRepository;

        public ProblemController(ApplicationDBContext context, IProblemRepository problemRepository)
        {
            _context = context;
            _problemRepository = problemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var problems = await _problemRepository.GetAllAsync();

            var problemDetailsDtos = problems.Select(p => p.ToProblemDetailsDto());

            return Ok(problemDetailsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var problem = await _problemRepository.GetByIdAsync(id);

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
            await _problemRepository.CreateAsync(problemModel);

            return CreatedAtAction(nameof(GetById), new { id = problemModel.Id }, problemModel.ToProblemDetailsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProblemRequestDto updateProblemRequestDto)
        {
            var problemModel = await _problemRepository.UpdateAsync(id, updateProblemRequestDto);

            if (problemModel == null)
            {
                return NotFound();
            }

            return Ok(problemModel.ToProblemDetailsDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var problemModel = await _problemRepository.DeleteAsync(id);

            if (problemModel == null)
            { 
                return NotFound();
            }

            return NoContent();
        }
    }
}