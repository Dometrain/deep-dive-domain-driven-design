using GymManagement.Domain.AdminAggregate;
using GymManagement.Domain.Common;
using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.AdminAggregate.Events;

public record SubscriptionSetEvent(Admin Admin, Subscription Subscription) : IDomainEvent;