using UserManagement.Domain.Common;

namespace UserManagement.Domain.UserAggregate.Events;

public record TrainerProfileCreatedEvent(Guid UserId, Guid TrainerId) : IDomainEvent;