using SessionReservation.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace SessionReservation.Application.Participants.Queries.ListParticipantSessions;

public record ListParticipantSessionsQuery(
        Guid ParticipantId,
        DateTime? StartDateTime = null,
        DateTime? EndDateTime = null) : IRequest<ErrorOr<List<Session>>>;