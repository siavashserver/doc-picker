using System.Net.Http.Headers;
using System.Text.Json;
using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Interfaces;
using Services.Shared.Extensions;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class UpdateSpecialityHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<UpdateSpecialityRequest, UpdateSpecialityResponse>
{
    public async Task<UpdateSpecialityResponse> Handle(UpdateSpecialityRequest request)
    {
        var speciality = new Speciality
        {
            Name = request.Name,
            Description = request.Description
        };

        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage =
            new HttpRequestMessage(HttpMethod.Post, $"{Speciality.IndexName}/_update/{request.SpecialityId}");

        var requestMessageContent = new
        {
            doc = speciality
        };

        requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestMessageContent),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);

        responseMessage.StatusCode.RaiseExceptionOnFailure();

        return new UpdateSpecialityResponse();
    }
}