using Grpc.Core;
using Services.Doctors.Core.RequestHandlers.Specialities;

namespace Services.Doctors.WebAPI;

public class SpecialitiesController(
    CreateDoctorHandler createSpecialityHandler,
    GetSpecialitiesHandler getSpecialitiesHandler,
    UpdateSpecialityHandler updateSpecialityHandler,
    DeleteSpecialityHandler deleteSpecialityHandler
) : Specialities.SpecialitiesBase
{
    public override async Task<CreateSpecialityResponse> CreateSpeciality(CreateSpecialityRequest request,
        ServerCallContext context)
    {
        return await createSpecialityHandler.Handle(request);
    }

    public override async Task<GetSpecialitiesResponse> GetSpecialities(GetSpecialitiesRequest request,
        ServerCallContext context)
    {
        return await getSpecialitiesHandler.Handle(request);
    }

    public override async Task<UpdateSpecialityResponse> UpdateSpeciality(UpdateSpecialityRequest request,
        ServerCallContext context)
    {
        return await updateSpecialityHandler.Handle(request);
    }

    public override async Task<DeleteSpecialityResponse> DeleteSpeciality(DeleteSpecialityRequest request,
        ServerCallContext context)
    {
        return await deleteSpecialityHandler.Handle(request);
    }
}