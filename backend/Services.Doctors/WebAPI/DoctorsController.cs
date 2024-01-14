using Grpc.Core;
using Services.Doctors.Core.RequestHandlers.Doctors;

namespace Services.Doctors.WebAPI;

public class DoctorsController(
    CreateDoctorHandler createDoctorHandler,
    GetDoctorsHandler getDoctorsHandler,
    UpdateDoctorHandler updateDoctorHandler,
    DeleteDoctorHandler deleteDoctorHandler
) : Doctors.DoctorsBase
{
    public override async Task<CreateDoctorResponse> CreateDoctor(CreateDoctorRequest request,
        ServerCallContext context)
    {
        return await createDoctorHandler.Handle(request);
    }

    public override async Task<GetDoctorsResponse> GetDoctors(GetDoctorsRequest request, ServerCallContext context)
    {
        return await getDoctorsHandler.Handle(request);
    }

    public override async Task<UpdateDoctorResponse> UpdateDoctor(UpdateDoctorRequest request,
        ServerCallContext context)
    {
        return await updateDoctorHandler.Handle(request);
    }

    public override async Task<DeleteDoctorResponse> DeleteDoctor(DeleteDoctorRequest request,
        ServerCallContext context)
    {
        return await deleteDoctorHandler.Handle(request);
    }
}