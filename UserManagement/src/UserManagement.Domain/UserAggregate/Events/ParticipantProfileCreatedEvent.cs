using UserManagement.Domain.Common;

namespace UserManagement.Domain.UserAggregate.Events;

public record ParticipantProfileCreatedEvent(Guid UserId, Guid ParticipantId) : IDomainEvent;