using System.Net.Http.Headers;
using System.Text.Json;
using Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;
using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Exceptions;
using Services.Shared.Core.Interfaces;

namespace Services.Doctors.Core.RequestHandlers.Doctors;

public class GetDoctorsHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<GetDoctorsRequest, GetDoctorsResponse>
{
    public async Task<GetDoctorsResponse> Handle(GetDoctorsRequest request)
    {
        if (0 < request.DoctorIds.Count) return await HandleDoctorIdsField(request);

        return await HandleOtherFields(request);
    }

    private async Task<GetDoctorsResponse> HandleDoctorIdsField(GetDoctorsRequest request)
    {
        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Doctor.IndexName}/_mget");
        var requestMessageContent = new
        {
            ids = request.DoctorIds
        };
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestMessageContent),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);
        var response = await responseMessage.Content.ReadFromJsonAsync<ElasticsearchMultiGetResponse<Doctor>>();

        if (response.docs.Any(document => false == document.found)) throw new NotFoundException();

        var doctorsResults = response.docs.Select(document => new GetDoctorsResult
        {
            Score = null,
            DoctorId = document._id,
            Name = document._source.Name,
            SpecialityId = document._source.SpecialityId
        });

        return new GetDoctorsResponse
        {
            HasNextPage = false,
            Doctors = { doctorsResults }
        };
    }

    private async Task<GetDoctorsResponse> HandleOtherFields(GetDoctorsRequest request)
    {
        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Doctor.IndexName}/_search");

        var requestMessageContent = new
        {
            from = request.Page * request.PageSize,
            size = request.PageSize,
            query = new
            {
                @bool = new
                {
                    should = new List<object>(),
                    filter = new List<object>()
                }
            }
        };

        requestMessageContent.query.@bool.should.AddRange(
            request.Names.SelectMany(name => new object[]
            {
                new ElasticsearchFuzzyQuery<Doctor>(new Doctor { Name = name }),
                new ElasticsearchWildcardQuery<Doctor>(new Doctor { Name = $"*{name}*" })
            })
        );

        if (0 < request.SpecialityIds.Count)
            requestMessageContent.query.@bool.filter.Add(
                new ElasticsearchTermsQuery<object>(new { SpecialityId = request.SpecialityIds })
            );

        requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestMessageContent),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);
        var response = await responseMessage.Content.ReadFromJsonAsync<ElasticsearchSearchResponse<Doctor>>();

        var doctorsResults = response.hits.hits.Select(hit => new GetDoctorsResult
        {
            Score = hit._score,
            DoctorId = hit._id,
            Name = hit._source.Name,
            SpecialityId = hit._source.SpecialityId
        });

        var hasNextPage = request.Page * request.PageSize + request.PageSize < response.hits.total.value;

        return new GetDoctorsResponse
        {
            HasNextPage = hasNextPage,
            Doctors = { doctorsResults }
        };
    }
}