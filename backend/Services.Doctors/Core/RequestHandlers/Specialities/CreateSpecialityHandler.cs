using System.Net.Http.Headers;
using System.Text.Json;
using Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;
using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Interfaces;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class CreateDoctorHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<CreateSpecialityRequest, CreateSpecialityResponse>
{
    public async Task<CreateSpecialityResponse> Handle(CreateSpecialityRequest request)
    {
        var speciality = new Speciality
        {
            Name = request.Name,
            Description = request.Description
        };

        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{Speciality.IndexName}/_doc");
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(speciality),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);
        var response = await responseMessage.Content.ReadFromJsonAsync<ElasticsearchCreateResponse>();

        return new CreateSpecialityResponse
        {
            SpecialityId = response._id
        };
    }
}