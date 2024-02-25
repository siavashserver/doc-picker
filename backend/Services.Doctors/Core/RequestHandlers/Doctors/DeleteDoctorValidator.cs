using FluentValidation;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class DeleteDoctorValidator : AbstractValidator<DeleteDoctorRequest>
{
    public DeleteDoctorValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty();
    }
}