using api.Data;
using api.DTO.Tag;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDBContext _context;

        public TagRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Tag> CreateAsync(Tag tagModel)
        {
            await _context.Tags.AddAsync(tagModel);
            await _context.SaveChangesAsync();

            return tagModel;
        }

        public async Task<Tag?> DeleteAsync(int id)
        {
            var tagModel = await _context.Tags.FindAsync(id);

            if (tagModel == null)
            {
                return null;
            }

            // DBSet doesn't have a RemoveAsync method in its definition,
            // I think DBContext is simply marking it to be deleted and doesn't actually access the database
            _context.Tags.Remove(tagModel);

            // This is when the program actually deletes the record from the database so we can utilize async function
            await _context.SaveChangesAsync();

            return tagModel;
        }

        public async Task<ICollection<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public Task<Tag?> GetByIdAsync(int id)
        {
            return _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAsync(int id, UpdateTagRequestDto updateTagRequestDto)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (existingTag == null)
            {
                return null;
            }

            existingTag.TagName = updateTagRequestDto.TagName;
            await _context.SaveChangesAsync();

            return existingTag;
        }
    }
}
