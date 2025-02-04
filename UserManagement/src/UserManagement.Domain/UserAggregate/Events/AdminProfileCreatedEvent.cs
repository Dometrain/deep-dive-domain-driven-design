using UserManagement.Domain.Common;

namespace UserManagement.Domain.UserAggregate.Events;

public record AdminProfileCreatedEvent(Guid UserId, Guid AdminId) : IDomainEvent;