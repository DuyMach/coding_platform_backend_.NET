using api.Data;
using api.DTO.Tag;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        { 
            var tags = _context.Tags.ToList();

            return Ok(tags);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var tag = _context.Tags.Find(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTagRequestDto createTagRequestDto)
        {
            if (createTagRequestDto == null)
            {
                return BadRequest("Tag name is required.");
            }

            var tagModel = createTagRequestDto.ToTagFromCreateDto();

            _context.Tags.Add(tagModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = tagModel.Id }, tagModel.ToTagDetailsDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody] UpdateTagRequestDto updateTagRequestDto)
        { 
            if (updateTagRequestDto == null)
            {
                return BadRequest("Tag data is required.");
            }

            var tagModel = _context.Tags.FirstOrDefault(t => t.Id == id);

            if (tagModel == null)
            {
                return NotFound();
            }

            tagModel.TagName = updateTagRequestDto.TagName;
            _context.SaveChanges();

            return Ok(tagModel.ToTagDetailsDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var tagModel = _context.Tags.FirstOrDefault(t => t.Id == id);

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
