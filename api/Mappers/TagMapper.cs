using api.DTO.Tag;
using api.Models;

namespace api.Mappers
{
    public static class TagMapper
    {
        public static Tag ToTagFromCreateDto(this CreateTagRequestDto createTagRequestDto)
        {
            return new Tag
            {
                TagName = createTagRequestDto.TagName
            };
        }
        public static TagDetailsDto ToTagDetailsDto(this Tag tagModel)
        {
            return new TagDetailsDto
            {
                Id = tagModel.Id,
                TagName = tagModel.TagName,
            };
        }
    }
}
