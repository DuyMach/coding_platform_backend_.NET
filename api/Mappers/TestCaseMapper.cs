using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.TestCase;
using api.Models;

namespace api.Mappers
{
    public static class TestCaseMapper
    {
        public static TestCase ToTestCaseFromCreateDto(this CreateTestCaseRequestDto createTestCaseRequestDto, int problemId)
        {
            return new TestCase
            {
                Input = createTestCaseRequestDto.Input,
                ExpectedOutput = createTestCaseRequestDto.ExpectedOutput,
                Explanation = createTestCaseRequestDto.Explanation,
                IsSample = createTestCaseRequestDto.IsSample,
                LanguageName = createTestCaseRequestDto.LanguageName,
                ProblemId = problemId
            };
        }

        public static TestCaseDetailsDto ToTestCaseDetailsDto(this TestCase testCase)
        {
            return new TestCaseDetailsDto
            {
                Id = testCase.Id,
                Input = testCase.Input,
                ExpectedOutput = testCase.ExpectedOutput,
                Explanation = testCase.Explanation,
                IsSample = testCase.IsSample,
                LanguageName = testCase.LanguageName,
                ProblemId = testCase.ProblemId
            };
        }
    }
}