using GymManagement.Domain.Common;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.SubscriptionAggregate.Events;

public record GymAddedEvent(Subscription Subscription, Gym Gym) : IDomainEvent;