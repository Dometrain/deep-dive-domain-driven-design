using ErrorOr;

namespace GymManagement.Domain.SubscriptionAggregate;

public static class SubscriptionErrors
{
    public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
        "Subscription.CannotHaveMoreGymsThanSubscriptionAllows",
        "A subscription cannot have more gyms than the subscription allows");
}