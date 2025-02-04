using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.SessionAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Sessions.Queries.GetSession;

public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, ErrorOr<Session>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ISessionsRepository _sessionsRepository;

    public GetSessionQueryHandler(ISessionsRepository sessionsRepository, IRoomsRepository roomsRepository)
    {
        _sessionsRepository = sessionsRepository;
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<Session>> Handle(GetSessionQuery query, CancellationToken cancellationToken)
    {
        var room = await _roomsRepository.GetByIdAsync(query.RoomId);

        if (room is null)
        {
            return Error.NotFound(description: "Room not found");
        }

        if (!room.HasSession(query.SessionId))
        {
            return Error.NotFound(description: "Session not found");
        }

        if (await _sessionsRepository.GetByIdAsync(query.SessionId) is not Session session)
        {
            return Error.NotFound(description: "Session not found");
        }

        return session;
    }
}
