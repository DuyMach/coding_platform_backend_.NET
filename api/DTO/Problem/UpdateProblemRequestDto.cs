using api.Enums;

namespace api.DTO.Problem
{
    public class UpdateProblemRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; }
        public Visibility Visibility { get; set; }
    }
}
