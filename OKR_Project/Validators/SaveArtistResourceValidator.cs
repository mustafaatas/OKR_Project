using API.DTO;
using FluentValidation;

namespace API.Validators
{
    public class SaveArtistResourceValidator: AbstractValidator<SaveArtistDTO>
    {
        public SaveArtistResourceValidator()
        {
            RuleFor(a => a.Name)
              .NotEmpty()
              .MaximumLength(50);
        }
    }
}
