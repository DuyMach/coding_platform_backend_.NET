using api.Data;
using api.DTO.Tag;
using api.Interfaces;
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
        private readonly ITagRepository _tagRepository;

        public TagController(ApplicationDBContext context, ITagRepository tagRepository)
        {
            _context = context;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var tags = await _tagRepository.GetAllAsync();

            var tagDtos = tags.Select(t => t.ToTagDetailsDto());

            return Ok(tagDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

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

            await _tagRepository.CreateAsync(tagModel);

            return CreatedAtAction(nameof(GetById), new { id = tagModel.Id }, tagModel.ToTagDetailsDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateTagRequestDto updateTagRequestDto)
        { 
            if (updateTagRequestDto == null)
            {
                return BadRequest("Tag data is required.");
            }

            var tagModel = await _tagRepository.UpdateAsync(id, updateTagRequestDto);

            if (tagModel == null)
            {
                return NotFound();
            }

            return Ok(tagModel.ToTagDetailsDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tagModel = await _tagRepository.DeleteAsync(id);

            if (tagModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
