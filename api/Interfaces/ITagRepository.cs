using api.DTO.Tag;
using api.Models;

namespace api.Interfaces
{
    public interface ITagRepository
    {
        Task<ICollection<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task<Tag> CreateAsync(Tag tagModel);
        Task<Tag?> UpdateAsync(int id, UpdateTagRequestDto updateTagRequestDto);
        Task<Tag?> DeleteAsync(int id);
    }
}
