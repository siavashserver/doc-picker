using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class UpdateSpecialityValidator : AbstractValidator<UpdateSpecialityRequest>
{
    public UpdateSpecialityValidator()
    {
        RuleFor(x => x.SpecialityId).NotEmpty();
        When(x => x.Name is not null, () => RuleFor(x => x.Name).NotEmpty());
        When(x => x.Description is not null, () => RuleFor(x => x.Description).NotEmpty());
    }
}