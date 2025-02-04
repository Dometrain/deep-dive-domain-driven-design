using GymManagement.Domain.Common;
using GymManagement.Domain.RoomAggregate;

namespace GymManagement.Domain.GymAggregate.Events;

public record RoomAddedEvent(Gym Gym, Room Room) : IDomainEvent;