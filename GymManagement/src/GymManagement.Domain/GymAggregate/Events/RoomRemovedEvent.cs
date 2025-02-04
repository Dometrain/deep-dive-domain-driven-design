using GymManagement.Domain.Common;

namespace GymManagement.Domain.GymAggregate.Events;

public record RoomRemovedEvent(Gym Gym, Guid RoomId) : IDomainEvent;