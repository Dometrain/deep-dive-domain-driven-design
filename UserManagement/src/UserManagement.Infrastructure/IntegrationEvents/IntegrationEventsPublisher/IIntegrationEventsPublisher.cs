using SharedKernel.IntegrationEvents;

namespace UserManagement.Api.IntegrationEvents.IntegrationEventsPublisher;

public interface IIntegrationEventsPublisher
{
    public void PublishEvent(IIntegrationEvent integrationEvent);
}