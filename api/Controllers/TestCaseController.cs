using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.TestCase;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/testcase")]
    [ApiController]
    public class TestCaseController : ControllerBase
    {
        private readonly ITestCaseRepository _testCaseRepository;
        private readonly IProblemRepository _problemRepository;
        public TestCaseController(ITestCaseRepository testCaseRepository, IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
            _testCaseRepository = testCaseRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var testCaseModel = await _testCaseRepository.GetByIdAsync(id);

            if (testCaseModel == null)
            {
                return NotFound();
            }

            return Ok(testCaseModel.ToTestCaseDetailsDto());
        }

        [HttpPost("{problemId:int}")]
        public async Task<IActionResult> Create([FromRoute] int problemId, [FromBody] CreateTestCaseRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _problemRepository.ProblemExists(problemId))
            {
                return BadRequest("Problem does not exist.");
            }

            var testCaseModel = requestDto.ToTestCaseFromCreateDto(problemId);
            await _testCaseRepository.CreateAsync(testCaseModel);

            return CreatedAtAction(nameof(GetById), new { id = testCaseModel.Id }, testCaseModel.ToTestCaseDetailsDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var testCases = await _testCaseRepository.GetAllAsync();

            var testCaseDetailsDto = testCases.Select(tc => tc.ToTestCaseDetailsDto());

            return Ok(testCaseDetailsDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateTestCaseRequestDto updateTestCaseRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var testCaseModel = await _testCaseRepository.UpdateAsync(id, updateTestCaseRequestDto);

            if (testCaseModel == null)
            {
                return NotFound();
            }

            return Ok(testCaseModel.ToTestCaseDetailsDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        { 
            var testCaseModel = await _testCaseRepository.DeleteAsync(id);

            if (testCaseModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}