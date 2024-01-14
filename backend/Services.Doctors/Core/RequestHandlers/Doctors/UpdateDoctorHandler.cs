using System.Net.Http.Headers;
using System.Text.Json;
using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Interfaces;
using Services.Shared.Extensions;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class UpdateDoctorHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<UpdateDoctorRequest, UpdateDoctorResponse>
{
    public async Task<UpdateDoctorResponse> Handle(UpdateDoctorRequest request)
    {
        var doctor = new Doctor
        {
            Name = request.Name,
            SpecialityId = request.SpecialityId
        };

        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage =
            new HttpRequestMessage(HttpMethod.Post, $"{Doctor.IndexName}/_update/{request.DoctorId}");

        var requestMessageContent = new
        {
            doc = doctor
        };

        requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestMessageContent),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);

        responseMessage.StatusCode.RaiseExceptionOnFailure();

        return new UpdateDoctorResponse();
    }
}