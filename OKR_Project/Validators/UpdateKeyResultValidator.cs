using API.DTO.KeyResult;
using FluentValidation;

namespace API.Validators
{
    public class UpdateKeyResultValidator : AbstractValidator<UpdateKeyResultDTO>
    {
        public UpdateKeyResultValidator()
        {
            RuleFor(k => k.ActualValue).GreaterThan(0).When(k => k.Status == "Completed");
        }
    }
}
