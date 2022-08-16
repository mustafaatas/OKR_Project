using API.DTO.Team;
using FluentValidation;

namespace API.Validators
{
    public class TeamResourceValidator : AbstractValidator<TeamDTO>
    {
        public TeamResourceValidator()
        {

        }
    }
}
