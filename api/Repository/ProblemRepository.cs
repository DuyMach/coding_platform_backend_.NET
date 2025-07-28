using api.Data;
using api.DTO.Problem;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly ApplicationDBContext _context;

        public ProblemRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Problem> CreateAsync(Problem problemModel)
        {
            await _context.Problems.AddAsync(problemModel);
            await _context.SaveChangesAsync();

            return problemModel;
        }

        public async Task<Problem?> DeleteAsync(int id)
        {
            var problemModel = await _context.Problems.FindAsync(id);

            if (problemModel == null)
            {
                return null;
            }

            _context.Problems.Remove(problemModel);
            await _context.SaveChangesAsync();

            return problemModel;
        }

        public async Task<ICollection<Problem>> GetAllAsync()
        {
            return await _context.Problems.ToListAsync();
        }

        public async Task<Problem?> GetByIdAsync(int id)
        {
            return await _context.Problems.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ProblemExists(int id)
        {
            return await _context.Problems.AnyAsync(p => p.Id == id);
        }

        public async Task<Problem?> UpdateAsync(int id, UpdateProblemRequestDto updateProblemRequestDto)
        {
            var existingProblem = await _context.Problems.FindAsync(id);

            if (existingProblem == null)
            {
                return null;
            }

            existingProblem.Title = updateProblemRequestDto.Title;
            existingProblem.Description = updateProblemRequestDto.Description;
            existingProblem.Difficulty = updateProblemRequestDto.Difficulty;
            existingProblem.Visibility = updateProblemRequestDto.Visibility;
            existingProblem.UpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingProblem;
        }
    }
}
