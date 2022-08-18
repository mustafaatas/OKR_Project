using API.DTO.Department;
using FluentValidation;

namespace API.Validators
{
    public class SaveDepartmentResourceValidator: AbstractValidator<SaveDepartmentDTO>
    {
        public SaveDepartmentResourceValidator()
        {

        }
    }
}
