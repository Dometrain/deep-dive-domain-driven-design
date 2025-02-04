using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Rooms.Queries.ListRooms;

public class ListRoomsQueryHandler : IRequestHandler<ListRoomsQuery, ErrorOr<List<Room>>>
{
    private readonly IRoomsRepository _roomsRepository;

    public ListRoomsQueryHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<List<Room>>> Handle(ListRoomsQuery query, CancellationToken cancellationToken)
    {
        return await _roomsRepository.ListByGymIdAsync(query.GymId);
    }
}
