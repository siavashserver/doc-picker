using System.Net.Http.Headers;
using System.Text.Json;
using Services.Doctors.Core.DataAccess.DTOs.Elasticsearch;
using Services.Doctors.Core.DataAccess.Entities;
using Services.Shared.Core.Exceptions;
using Services.Shared.Core.Interfaces;

namespace Services.Doctors.Core.RequestHandlers.Specialities;

public class GetSpecialitiesHandler(
    IHttpClientFactory httpClientFactory
) : IRequestHandler<GetSpecialitiesRequest, GetSpecialitiesResponse>
{
    public async Task<GetSpecialitiesResponse> Handle(GetSpecialitiesRequest request)
    {
        if (0 < request.SpecialityIds.Count) return await HandleSpecialityIdsField(request);

        return await HandleOtherFields(request);
    }

    private async Task<GetSpecialitiesResponse> HandleSpecialityIdsField(GetSpecialitiesRequest request)
    {
        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Speciality.IndexName}/_mget");
        var requestMessageContent = new
        {
            ids = request.SpecialityIds
        };
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestMessageContent),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);
        var response = await responseMessage.Content.ReadFromJsonAsync<ElasticsearchMultiGetResponse<Speciality>>();

        if (response.docs.Any(document => false == document.found)) throw new NotFoundException();

        var specialitiesResults = response.docs.Select(document => new GetSpecialitiesResult
        {
            Score = null,
            SpecialityId = document._id,
            Name = document._source.Name,
            Description = document._source.Description
        });

        return new GetSpecialitiesResponse
        {
            HasNextPage = false,
            Specialities = { specialitiesResults }
        };
    }

    private async Task<GetSpecialitiesResponse> HandleOtherFields(GetSpecialitiesRequest request)
    {
        var httpClient = httpClientFactory.CreateClient("elasticsearch");
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Speciality.IndexName}/_search");

        var requestMessageContent = new
        {
            from = request.Page * request.PageSize,
            size = request.PageSize,
            query = new
            {
                @bool = new
                {
                    should = new List<object>()
                }
            }
        };

        requestMessageContent.query.@bool.should.AddRange(
            request.Names.SelectMany(name => new object[]
            {
                new ElasticsearchFuzzyQuery<Speciality>(new Speciality { Name = name }),
                new ElasticsearchWildcardQuery<Speciality>(new Speciality { Name = $"*{name}*" })
            })
        );

        requestMessageContent.query.@bool.should.AddRange(
            request.Descriptions.SelectMany(description => new object[]
            {
                new ElasticsearchFuzzyQuery<Speciality>(new Speciality { Description = description }),
                new ElasticsearchWildcardQuery<Speciality>(new Speciality { Description = $"*{description}*" })
            })
        );

        requestMessage.Content = new StringContent(JsonSerializer.Serialize(requestMessageContent),
            new MediaTypeHeaderValue("application/json"));
        var responseMessage = await httpClient.SendAsync(requestMessage);
        var response = await responseMessage.Content.ReadFromJsonAsync<ElasticsearchSearchResponse<Speciality>>();

        var specialitiesResults = response.hits.hits.Select(hit => new GetSpecialitiesResult
        {
            Score = hit._score,
            SpecialityId = hit._id,
            Name = hit._source.Name,
            Description = hit._source.Description
        });

        var hasNextPage = request.Page * request.PageSize + request.PageSize < response.hits.total.value;

        return new GetSpecialitiesResponse
        {
            HasNextPage = hasNextPage,
            Specialities = { specialitiesResults }
        };
    }
}