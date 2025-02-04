using ErrorOr;

using GymManagement.Domain.SubscriptionAggregate;

using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId)
    : IRequest<ErrorOr<Subscription>>;