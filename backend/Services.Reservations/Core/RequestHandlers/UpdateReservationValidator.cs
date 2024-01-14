using FluentValidation;

namespace Services.Reservations.Core.RequestHandlers;

public class UpdateReservationValidator : AbstractValidator<UpdateReservationRequest>
{
    public UpdateReservationValidator()
    {
        RuleFor(x => x.ReservationId).NotEmpty();
        When(x => x.PatientId is not null, () => RuleFor(x => x.PatientId).NotEmpty());
        When(x => x.DoctorId is not null, () => RuleFor(x => x.DoctorId).NotEmpty());
        When(x => x.Date is not null, () => RuleFor(x => x.Date).NotEmpty());
        When(x => x.Shift is not null, () => RuleFor(x => x.Shift).NotEmpty());
    }
}