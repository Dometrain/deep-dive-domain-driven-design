using SessionReservation.Domain.RoomAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Rooms.Queries.GetRoom;

public record GetRoomQuery(
    Guid GymId,
    Guid RoomId) : IRequest<ErrorOr<Room>>;