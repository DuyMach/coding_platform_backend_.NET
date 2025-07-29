using api.Enums;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.TestCase
{
    public class UpdateTestCaseRequestDto
    {
        [Required]
        public string Input { get; set; } = string.Empty;

        [Required]
        [MaxLength(280, ErrorMessage = "Expected output must be under 280 characters.")]
        public string ExpectedOutput { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Explanation must be detailed and greater than 10 characters.")]
        public string Explanation { get; set; } = string.Empty;

        // Not required since it is default to false
        public bool IsSample { get; set; } = false;

        [Required]
        public LanguageName LanguageName { get; set; }
    }
}
