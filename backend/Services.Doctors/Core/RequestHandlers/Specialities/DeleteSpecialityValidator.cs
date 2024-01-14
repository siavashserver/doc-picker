using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class DeleteSpecialityValidator : AbstractValidator<DeleteSpecialityRequest>
{
    public DeleteSpecialityValidator()
    {
        RuleFor(x => x.SpecialityId).NotEmpty();
    }
}