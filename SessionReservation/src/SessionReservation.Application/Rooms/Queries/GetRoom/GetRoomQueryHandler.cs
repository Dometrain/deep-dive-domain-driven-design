using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Rooms.Queries.GetRoom;

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, ErrorOr<Room>>
{
    private readonly IRoomsRepository _roomsRepository;

    public GetRoomQueryHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<Room>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        return await _roomsRepository.GetByIdAsync(query.RoomId) is not Room room
            ? Error.NotFound(description: "Room not found")
            : room;
    }
}