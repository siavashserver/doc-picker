using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class CreateDoctorValidator : AbstractValidator<CreateSpecialityRequest>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}