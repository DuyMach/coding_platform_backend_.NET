using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.TestCase;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private readonly ApplicationDBContext _context;
        public TestCaseRepository(ApplicationDBContext context)
        {
            _context = context;
            
        }
        public async Task<TestCase> CreateAsync(TestCase testCaseModel)
        {
            await _context.TestCases.AddAsync(testCaseModel);
            await _context.SaveChangesAsync();

            return testCaseModel;
        }

        public async Task<TestCase?> DeleteAsync(int id)
        {
            var testCaseModel = await _context.TestCases.FirstOrDefaultAsync(tc => tc .Id == id);

            if (testCaseModel == null)
            {
                return null;
            }

            _context.TestCases.Remove(testCaseModel);
            await _context.SaveChangesAsync();

            return testCaseModel;
        }

        public async Task<ICollection<TestCase>> GetAllAsync()
        {
            return await _context.TestCases.ToListAsync();
        }

        public async Task<TestCase?> GetByIdAsync(int id)
        {
            return await _context.TestCases.FirstOrDefaultAsync(t => t.Id == id); 
        }

        public async Task<TestCase?> UpdateAsync(int id, UpdateTestCaseRequestDto updateTestCaseRequestDto)
        {
            var existingTestCase = await _context.TestCases.FirstOrDefaultAsync(tc => tc.Id == id);

            if (existingTestCase == null)
            {
                return null;
            }

            existingTestCase.Explanation = updateTestCaseRequestDto.Explanation;
            existingTestCase.IsSample = updateTestCaseRequestDto.IsSample;
            existingTestCase.ExpectedOutput = updateTestCaseRequestDto.ExpectedOutput;
            existingTestCase.Input = updateTestCaseRequestDto.Input;

            await _context.SaveChangesAsync();
            return existingTestCase;
        }
    }
}