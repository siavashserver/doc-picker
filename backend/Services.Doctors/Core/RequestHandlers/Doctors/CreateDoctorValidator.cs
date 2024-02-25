using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class CreateDoctorValidator : AbstractValidator<CreateDoctorRequest>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.SpecialityId).NotEmpty();
    }
}