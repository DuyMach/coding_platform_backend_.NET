using api.Enums;

namespace api.DTO.Problem
{
    public class CreateProblemRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; }
        public Difficulty Difficulty { get; set; }
        public Visibility Visibility { get; set; }
    }
}
