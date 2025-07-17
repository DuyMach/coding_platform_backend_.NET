using api.DTO.Problem;
using api.Models;

namespace api.Interfaces
{
    public interface IProblemRepository
    {
        Task<ICollection<Problem>> GetAllAsync();
        Task<Problem?> GetByIdAsync(int id);
        Task<Problem> CreateAsync(Problem problemModel);
        Task<Problem?> UpdateAsync(int id, UpdateProblemRequestDto updateProblemRequestDto);
        Task<Problem?> DeleteAsync(int id);
    }
}
