using SharedKernel.IntegrationEvents;

namespace GymManagement.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;

public interface IIntegrationEventsPublisher
{
    public void PublishEvent(IIntegrationEvent integrationEvent);
}