using FluentValidation;

namespace Services.Reservations.Core.RequestHandlers;

public class DeleteReservationValidator : AbstractValidator<DeleteReservationRequest>
{
    public DeleteReservationValidator()
    {
        RuleFor(x => x.ReservationId).NotEmpty();
    }
}