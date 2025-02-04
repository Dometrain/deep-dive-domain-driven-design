using ErrorOr;

using GymManagement.Domain.GymAggregate;

using MediatR;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Gym>>>;