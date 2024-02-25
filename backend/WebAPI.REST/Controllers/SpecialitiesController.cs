using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Services.Doctors;
using WebAPI.REST.DTOs.Specialities;

namespace WebAPI.REST.Controllers;

public class SpecialitiesController(GrpcClientFactory grpcClientFactory) : BaseAPIController
{
    private readonly Specialities.SpecialitiesClient _specialitiesClient =
        grpcClientFactory.CreateClient<Specialities.SpecialitiesClient>(nameof(Specialities.SpecialitiesClient));

    [HttpPost]
    public async Task<ActionResult<CreateSpecialityResponseDTO>> CreateSpeciality(CreateSpecialityRequestDTO request)
    {
        var response = await _specialitiesClient.CreateSpecialityAsync(request);
        return CreatedAtRoute(
            nameof(GetSpeciality),
            new { id = response.SpecialityId },
            response);
    }

    [HttpGet("{id}", Name = nameof(GetSpeciality))]
    public async Task<ActionResult<GetSpecialityResponseDTO>> GetSpeciality(string id)
    {
        var request = new GetSpecialitiesRequest { Page = 0, PageSize = 1, SpecialityIds = { id } };
        var response = await _specialitiesClient.GetSpecialitiesAsync(request);
        return Ok(response);
    }

    [HttpPost("search")]
    public async Task<ActionResult<GetSpecialitiesResponseDTO>> GetSpecialities(GetSpecialitiesRequestDTO request)
    {
        var response = await _specialitiesClient.GetSpecialitiesAsync(request);
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateSpeciality(string id, UpdateSpecialityRequestDTO request)
    {
        UpdateSpecialityRequest _request = request;
        _request.SpecialityId = id;
        var response = await _specialitiesClient.UpdateSpecialityAsync(_request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSpeciality(string id)
    {
        var response =
            await _specialitiesClient.DeleteSpecialityAsync(new DeleteSpecialityRequest { SpecialityId = id });
        return NoContent();
    }
}