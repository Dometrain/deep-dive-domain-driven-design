using SessionReservation.Application.Participants.Commands.CancelReservation;
using SessionReservation.Application.Participants.Queries.ListParticipantSessions;
using SessionReservation.Application.Reservations.Commands.CreateReservation;
using SessionReservation.Contracts.Sessions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace SessionReservation.Api.Controllers;

[Route("participants")]
public class ParticipantsController : ApiController
{
    private readonly ISender _sender;

    public ParticipantsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{participantId:guid}/sessions")]
    public async Task<IActionResult> ListParticipantSessions(
        Guid participantId,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null)
    {
        var query = new ListParticipantSessionsQuery(
            participantId,
            startDateTime,
            endDateTime);

        var listParticipantSessionsResult = await _sender.Send(query);

        return listParticipantSessionsResult.Match(
            sessions => Ok(sessions.ConvertAll(session => new SessionResponse(
                session.Id,
                session.Name,
                session.Description,
                session.NumParticipants,
                session.MaxParticipants,
                session.Date.ToDateTime(session.Time.Start),
                session.Date.ToDateTime(session.Time.End),
                session.Categories.Select(category => category.Name).ToList()))),
            Problem);
    }

    [HttpDelete("{participantId:guid}/sessions/{sessionId:guid}/reservation")]
    public async Task<IActionResult> CancelReservation(
        Guid participantId,
        Guid sessionId)
    {
        var command = new CancelReservationCommand(participantId, sessionId);

        var cancelReservationResult = await _sender.Send(command);

        return cancelReservationResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPost("{participantId:guid}/sessions/{sessionId:guid}/reservation")]
    public async Task<IActionResult> CreateReservation(
        Guid participantId,
        Guid sessionId)
    {
        var command = new CreateReservationCommand(sessionId, participantId);

        var cancelReservationResult = await _sender.Send(command);

        return cancelReservationResult.Match(
            _ => NoContent(),
            Problem);
    }
}