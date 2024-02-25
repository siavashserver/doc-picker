using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Services.Doctors;
using WebAPI.REST.DTOs.Doctors;

namespace WebAPI.REST.Controllers;

public class DoctorsController(GrpcClientFactory grpcClientFactory) : BaseAPIController
{
    private readonly Doctors.DoctorsClient _doctorsClient =
        grpcClientFactory.CreateClient<Doctors.DoctorsClient>(nameof(Doctors.DoctorsClient));

    [HttpPost]
    public async Task<ActionResult<CreateDoctorResponseDTO>> CreateDoctor(CreateDoctorRequestDTO request)
    {
        var response = await _doctorsClient.CreateDoctorAsync(request);
        return CreatedAtRoute(
            nameof(GetDoctor),
            new { id = response.DoctorId },
            response);
    }

    [HttpGet("{id}", Name = nameof(GetDoctor))]
    public async Task<ActionResult<GetDoctorResponseDTO>> GetDoctor(string id)
    {
        var request = new GetDoctorsRequest { Page = 0, PageSize = 1, DoctorIds = { id } };
        var response = await _doctorsClient.GetDoctorsAsync(request);
        return Ok(response);
    }

    [HttpPost("search")]
    public async Task<ActionResult<GetDoctorsResponseDTO>> GetDoctors(GetDoctorsRequestDTO request)
    {
        var response = await _doctorsClient.GetDoctorsAsync(request);
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateDoctor(string id, UpdateDoctorRequestDTO request)
    {
        UpdateDoctorRequest _request = request;
        _request.DoctorId = id;
        var response = await _doctorsClient.UpdateDoctorAsync(_request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDoctor(string id)
    {
        var response = await _doctorsClient.DeleteDoctorAsync(new DeleteDoctorRequest { DoctorId = id });
        return NoContent();
    }
}