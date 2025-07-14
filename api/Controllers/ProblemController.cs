using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Problem;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var problems = _context.Problems.ToList();

            var problemDetailsDtos = problems.Select(p => p.ToProblemDetailsDto());

            return Ok(problemDetailsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var problem = _context.Problems.Find(id);

            if (problem == null)
            {
                return NotFound();
            }

            return Ok(problem);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProblemRequestDto createProblemRequestDto)
        { 
            if (createProblemRequestDto == null)
            {
                return BadRequest("Problem data is required.");
            }

            var problemModel = createProblemRequestDto.ToProblemFromCreateDto();
            _context.Problems.Add(problemModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = problemModel.Id }, problemModel.ToProblemDetailsDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateProblemRequestDto updateProblemRequestDto)
        {
            var problemModel = _context.Problems.FirstOrDefault(p => p.Id == id);

            if (problemModel == null)
            {
                return NotFound();
            }

            problemModel.Title = updateProblemRequestDto.Title;
            problemModel.Description = updateProblemRequestDto.Description;
            problemModel.Difficulty = updateProblemRequestDto.Difficulty;
            problemModel.Visibility = updateProblemRequestDto.Visibility;
            problemModel.UpdatedOn = DateTime.Now;

            _context.SaveChanges();

            return Ok(problemModel.ToProblemDetailsDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var problemModel = _context.Problems.FirstOrDefault(p => p.Id == id);

            if (problemModel == null)
            { 
                return NotFound();
            }

            _context.Problems.Remove(problemModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}