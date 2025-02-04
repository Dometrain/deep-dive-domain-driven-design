using ErrorOr;

using GymManagement.Domain.SubscriptionAggregate;

using MediatR;

namespace GymManagement.Application.Subscriptions.Queries.ListSubscriptions;

// TODO: add admin id, for now, return all
public record ListSubscriptionsQuery() : IRequest<ErrorOr<List<Subscription>>>;