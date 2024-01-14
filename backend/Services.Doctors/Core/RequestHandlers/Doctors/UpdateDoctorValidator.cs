using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorRequest>
{
    public UpdateDoctorValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
        When(x => x.Name is not null, () => RuleFor(x => x.Name).NotEmpty());
        When(x => x.SpecialityId is not null, () => RuleFor(x => x.SpecialityId).NotEmpty());
    }
}