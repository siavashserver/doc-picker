using FluentValidation;

namespace Services.Reservations.Core.RequestHandlers;

public class CreateReservationValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Shift).NotEmpty();
    }
}