using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class GetSpecialitiesValidator : AbstractValidator<GetSpecialitiesRequest>
{
    public GetSpecialitiesValidator()
    {
        RuleFor(x => x.Page).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotNull().GreaterThanOrEqualTo(1);
    }
}