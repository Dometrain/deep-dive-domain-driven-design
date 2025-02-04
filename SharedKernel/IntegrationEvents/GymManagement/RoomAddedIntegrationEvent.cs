namespace SharedKernel.IntegrationEvents.GymManagement;

public record RoomAddedIntegrationEvent(
    string Name,
    Guid RoomId,
    Guid GymId,
    int MaxDailySessions) : IIntegrationEvent;
