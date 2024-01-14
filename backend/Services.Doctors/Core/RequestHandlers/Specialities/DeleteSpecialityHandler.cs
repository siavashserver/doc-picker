using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Interfaces;
using Services.Shared.Extensions;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class DeleteSpecialityHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<DeleteSpecialityRequest, DeleteSpecialityResponse>
{
    public async Task<DeleteSpecialityResponse> Handle(DeleteSpecialityRequest request)
    {
        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage =
            new HttpRequestMessage(HttpMethod.Delete, $"{Speciality.IndexName}/_doc/{request.SpecialityId}");
        var responseMessage = await httpClient.SendAsync(requestMessage);

        responseMessage.StatusCode.RaiseExceptionOnFailure();

        return new DeleteSpecialityResponse();
    }
}