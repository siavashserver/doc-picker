using System.Net.Http.Headers;
using System.Text.Json;
using Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;
using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Interfaces;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class CreateDoctorHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<CreateDoctorRequest, CreateDoctorResponse>
{
    public async Task<CreateDoctorResponse> Handle(CreateDoctorRequest request)
    {
        var doctor = new Doctor
        {
            Name = request.Name,
            SpecialityId = request.SpecialityId
        };

        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{Doctor.IndexName}/_doc");
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(doctor),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);
        var response = await responseMessage.Content.ReadFromJsonAsync<ElasticsearchCreateResponse>();

        return new CreateDoctorResponse
        {
            DoctorId = response._id
        };
    }
}