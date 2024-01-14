using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Services.Reservations;
using WebAPI.REST.DTOs.Reservations;

namespace WebAPI.REST.Controllers;

public class ReservationsController(GrpcClientFactory grpcClientFactory) : BaseAPIController
{
    private readonly Reservations.ReservationsClient _reservationsClient =
        grpcClientFactory.CreateClient<Reservations.ReservationsClient>(nameof(Reservations.ReservationsClient));

    [HttpPost]
    public async Task<ActionResult<CreateReservationResponseDTO>> CreateReservation(CreateReservationRequestDTO request)
    {
        var response = await _reservationsClient.CreateReservationAsync(request);
        return CreatedAtRoute(nameof(GetReservation), new { id = response.ReservationId }, response);
    }

    [HttpGet("{id:int}", Name = nameof(GetReservation))]
    public async Task<ActionResult<GetReservationResponseDTO>> GetReservation(int id)
    {
        var request = new GetReservationsRequest { Page = 0, PageSize = 1, ReservationIds = { id } };
        var response = await _reservationsClient.GetReservationsAsync(request);
        return Ok(response);
    }

    [HttpPost("search")]
    public async Task<ActionResult<GetReservationsResponseDTO>> GetReservations(GetReservationsRequestDTO request)
    {
        var response = await _reservationsClient.GetReservationsAsync(request);
        return Ok(response);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateReservation(int id, UpdateReservationRequestDTO request)
    {
        UpdateReservationRequest _request = request;
        _request.ReservationId = id;
        var response = await _reservationsClient.UpdateReservationAsync(_request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteReservation(int id)
    {
        var response =
            await _reservationsClient.DeleteReservationAsync(new DeleteReservationRequest { ReservationId = id });
        return NoContent();
    }
}