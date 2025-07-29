using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTO.TestCase
{
    public class CreateTestCaseRequestDto
    {
        [Required]
        public string Input { get; set; } = string.Empty;

        [Required]
        [MaxLength(280, ErrorMessage = "Expected output cannot be over 280 characters.")]
        public string ExpectedOutput { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Explanation must be detailed and greater than 10 characters.")]
        public string Explanation { get; set; } = string.Empty;

        // Not required since we default to false
        public bool IsSample { get; set; } = false;

        // still figuring out how I want to implement ENUM validation
        [Required]
        public LanguageName LanguageName { get; set; }
    }
}