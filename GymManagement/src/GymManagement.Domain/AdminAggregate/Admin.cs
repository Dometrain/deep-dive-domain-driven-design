using GymManagement.Domain.AdminAggregate.Events;

using GymManagement.Domain.Common;
using GymManagement.Domain.SubscriptionAggregate;

using Throw;

namespace GymManagement.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; } = null;

    public Admin(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    private Admin() { }

    public void SetSubscription(Subscription subscription)
    {
        SubscriptionId.HasValue.Throw().IfTrue();

        SubscriptionId = subscription.Id;

        _domainEvents.Add(new SubscriptionSetEvent(this, subscription));
    }
}