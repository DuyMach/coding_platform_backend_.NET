using api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; }
        public Difficulty Difficulty { get; set; }
        public Visibility Visibility { get; set; }
        public ICollection<ProblemTag> ProblemTags { get; set; } = [];
        public ICollection<TestCase> TestCases { get; set; } = [];
    }
}