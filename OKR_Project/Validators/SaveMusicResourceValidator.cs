using API.DTO;
using FluentValidation;

namespace API.Validators
{
    public class SaveMusicResourceValidator: AbstractValidator<SaveMusicDTO>
    {
        public SaveMusicResourceValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.ArtistId)
                .NotEmpty()
                .WithMessage("'Artist Id' must not be 0.");
        }
    }
}