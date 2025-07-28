using api.Enums;

namespace api.DTO.TestCase
{
    public class UpdateTestCaseRequestDto
    {
        public string Input { get; set; } = string.Empty;
        public string ExpectedOutput { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public bool IsSample { get; set; } = false;
        public LanguageName LanguageName { get; set; }
    }
}
