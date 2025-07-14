using api.DTO.Problem;
using api.Models;

namespace api.Mappers
{
    public static class ProblemMapper
    {
        public static ProblemDetailsDto ToProblemDetailsDto(this Problem problemModel)
        {
            return new ProblemDetailsDto
            {
                Id = problemModel.Id,
                Title = problemModel.Title,
                Description = problemModel.Description,
                CreatedOn = problemModel.CreatedOn,
                UpdatedOn = problemModel.UpdatedOn,
                Difficulty = problemModel.Difficulty,
                Visibility = problemModel.Visibility
            };
        }

        public static Problem ToProblemFromCreateDto(this CreateProblemRequestDto createProblemRequestDto)
        {
            return new Problem
            {
                Title = createProblemRequestDto.Title,
                Description = createProblemRequestDto.Description,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Difficulty = createProblemRequestDto.Difficulty,
                Visibility = createProblemRequestDto.Visibility
            };
        }
    }
}
