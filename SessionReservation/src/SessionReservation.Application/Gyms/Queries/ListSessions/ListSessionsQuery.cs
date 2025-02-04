using SessionReservation.Domain.SessionAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Gyms.Queries.ListSessions;

public record ListSessionsQuery(
    Guid GymId,
    DateTime? StartDateTime = null,
    DateTime? EndDateTime = null,
    List<SessionCategory>? Categories = null) : IRequest<ErrorOr<List<Session>>>;