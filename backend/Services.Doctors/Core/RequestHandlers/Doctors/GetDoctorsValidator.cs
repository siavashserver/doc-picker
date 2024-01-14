using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class GetDoctorsValidator : AbstractValidator<GetDoctorsRequest>
{
    public GetDoctorsValidator()
    {
        RuleFor(x => x.Page).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotNull().GreaterThanOrEqualTo(1);
    }
}