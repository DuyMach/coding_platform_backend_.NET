using api.Data;
using api.DTO.Tag;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TagController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var tags = await _context.Tags.ToListAsync();

            var tagDtos = tags.Select(t => t.ToTagDetailsDto());

            return Ok(tagDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag.ToTagDetailsDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTagRequestDto createTagRequestDto)
        {
            if (createTagRequestDto == null)
            {
                return BadRequest("Tag name is required.");
            }

            var tagModel = createTagRequestDto.ToTagFromCreateDto();

            await _context.Tags.AddAsync(tagModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = tagModel.Id }, tagModel.ToTagDetailsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateTagRequestDto updateTagRequestDto)
        { 
            if (updateTagRequestDto == null)
            {
                return BadRequest("Tag data is required.");
            }

            var tagModel = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tagModel == null)
            {
                return NotFound();
            }

            tagModel.TagName = updateTagRequestDto.TagName;
            await _context.SaveChangesAsync();

            return Ok(tagModel.ToTagDetailsDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tagModel = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tagModel == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tagModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
