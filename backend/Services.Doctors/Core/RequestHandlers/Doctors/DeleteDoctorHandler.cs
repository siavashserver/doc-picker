using MassTransit;
using Services.Doctors.Core.DataAccess.Entities;
using Services.Doctors.Shared.Events;
using Services.Shared.Core.Interfaces;
using Services.Shared.Extensions;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class DeleteDoctorHandler(
    IHttpClientFactory httpClientFactory,
    IPublishEndpoint publishEndpoint
) : IRequestHandler<DeleteDoctorRequest, DeleteDoctorResponse>
{
    public async Task<DeleteDoctorResponse> Handle(DeleteDoctorRequest request)
    {
        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{Doctor.IndexName}/_doc/{request.DoctorId}");
        var responseMessage = await httpClient.SendAsync(requestMessage);

        responseMessage.StatusCode.RaiseExceptionOnFailure();

        await publishEndpoint.Publish<DoctorDeletedEvent>(new { request.DoctorId });

        return new DeleteDoctorResponse();
    }
}