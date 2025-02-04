using SessionReservation.Domain.RoomAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Rooms.Queries.ListRooms;

public record ListRoomsQuery(Guid GymId) : IRequest<ErrorOr<List<Room>>>;