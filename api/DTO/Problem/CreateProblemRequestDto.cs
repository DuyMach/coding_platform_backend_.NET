using api.Enums;
using System.ComponentModel.DataAnnotations;

namespace api.DTO.Problem
{
    public class CreateProblemRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must have at least 5 characters.")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Title must have at least 5 characters.")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters.")]
        public string Description { get; set; } = string.Empty;


        // Still trying to figure how I want to handle ENUM validation..
        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public Visibility Visibility { get; set; } 
    }
}
