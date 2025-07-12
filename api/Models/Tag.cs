using api.Enums;

namespace api.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public TagName TagName { get; set; }
        public ICollection<ProblemTag> ProblemTags { get; set; } = [];
    }
}
