using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.TestCase;
using api.Models;

namespace api.Interfaces
{
    public interface ITestCaseRepository
    {
        Task<ICollection<TestCase>> GetAllAsync();
        Task<TestCase?> GetByIdAsync(int id);
        Task<TestCase> CreateAsync(TestCase testCaseModel);
        Task <TestCase?> UpdateAsync(int id, UpdateTestCaseRequestDto updateTestCaseRequestDto);
        Task <TestCase?> DeleteAsync(int id);
    }
}