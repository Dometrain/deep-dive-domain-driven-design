namespace SharedKernel.IntegrationEvents.GymManagement;

public record RoomRemovedIntegrationEvent(Guid RoomId) : IIntegrationEvent;