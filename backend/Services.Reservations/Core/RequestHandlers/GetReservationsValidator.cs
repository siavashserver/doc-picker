using FluentValidation;

namespace Services.Reservations.Core.RequestHandlers;

public class GetReservationsValidator : AbstractValidator<GetReservationsRequest>
{
    public GetReservationsValidator()
    {
        RuleFor(x => x.Page).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotNull().GreaterThanOrEqualTo(1);
        RuleFor(x => x.Dates).NotNull();
        RuleFor(x => x.Shifts).NotNull();
        RuleFor(x => x.Dates.Start).NotNull();
        RuleFor(x => x.Shifts.Start).NotNull();
    }
}