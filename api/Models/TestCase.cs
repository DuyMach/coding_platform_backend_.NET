using api.Enums;

namespace api.Models
{
    public class TestCase
    {
        public int Id { get; set; }
        public string Input { get; set; } = string.Empty;
        public string ExpectedOutput { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public bool IsSample { get; set; } = false;
        public LanguageName LanguageName { get; set; }
        public Problem Problem { get; set; } = null!;
        public int ProblemId { get; set; }
    }
}
