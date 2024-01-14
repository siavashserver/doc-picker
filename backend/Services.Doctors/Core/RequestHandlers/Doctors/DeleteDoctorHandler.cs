using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Interfaces;
using Services.Shared.Extensions;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class DeleteDoctorHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<DeleteDoctorRequest, DeleteDoctorResponse>
{
    public async Task<DeleteDoctorResponse> Handle(DeleteDoctorRequest request)
    {
        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{Doctor.IndexName}/_doc/{request.DoctorId}");
        var responseMessage = await httpClient.SendAsync(requestMessage);

        responseMessage.StatusCode.RaiseExceptionOnFailure();

        return new DeleteDoctorResponse();
    }
}