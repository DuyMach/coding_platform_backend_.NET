using api.Enums;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.Tag
{
    public class CreateTagRequestDto
    {
        public TagName TagName { get; set; }
    }
}
