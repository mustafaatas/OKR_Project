using API.DTO.Objective;
using FluentValidation;

namespace API.Validators
{
    public class SaveObjectiveResourceValidator : AbstractValidator<SaveObjectiveDTO>
    {
        public SaveObjectiveResourceValidator()
        {

        }
    }
}