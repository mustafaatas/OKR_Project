using API.DTO.Team;
using FluentValidation;

namespace API.Validators
{
    public class SaveTeamResourceValidator : AbstractValidator<SaveTeamDTO>
    {
        public SaveTeamResourceValidator()
        {

        }
    }
}
