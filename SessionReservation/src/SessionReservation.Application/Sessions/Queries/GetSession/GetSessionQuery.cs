using SessionReservation.Domain.SessionAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Sessions.Queries.GetSession;

public record GetSessionQuery(Guid RoomId, Guid SessionId)
    : IRequest<ErrorOr<Session>>;