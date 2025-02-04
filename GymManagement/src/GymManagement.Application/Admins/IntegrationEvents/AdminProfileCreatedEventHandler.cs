using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.AdminAggregate;

using MediatR;

using SharedKernel.IntegrationEvents.UserManagement;

namespace GymManagement.Application.Admins.IntegrationEvents;

public class AdminProfileCreatedEventHandler : INotificationHandler<AdminProfileCreatedIntegrationEvent>
{
    private readonly IAdminsRepository _adminsRepository;

    public AdminProfileCreatedEventHandler(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task Handle(AdminProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var admin = new Admin(notification.UserId, id: notification.AdminId);

        await _adminsRepository.AddAdminAsync(admin);
    }
}
