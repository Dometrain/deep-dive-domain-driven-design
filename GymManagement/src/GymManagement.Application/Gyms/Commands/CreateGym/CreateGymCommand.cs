using ErrorOr;

using GymManagement.Domain.GymAggregate;

using MediatR;

namespace GymManagement.Application.Gyms.CreateGym;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;