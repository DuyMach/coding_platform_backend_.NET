using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTO.TestCase
{
    public class TestCaseDetailsDto
    {
         public int Id { get; set; }
        public string Input { get; set; } = string.Empty;
        public string ExpectedOutput { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public bool IsSample { get; set; } = false;
        public LanguageName LanguageName { get; set; }
        public int ProblemId { get; set; }
    }
}