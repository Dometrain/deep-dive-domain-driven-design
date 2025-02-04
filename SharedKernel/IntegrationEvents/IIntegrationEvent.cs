using System.Text.Json.Serialization;

using MediatR;

using SharedKernel.IntegrationEvents.GymManagement;
using SharedKernel.IntegrationEvents.UserManagement;

namespace SharedKernel.IntegrationEvents;

[JsonDerivedType(typeof(AdminProfileCreatedIntegrationEvent), typeDiscriminator: nameof(AdminProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(ParticipantProfileCreatedIntegrationEvent), typeDiscriminator: nameof(ParticipantProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(TrainerProfileCreatedIntegrationEvent), typeDiscriminator: nameof(TrainerProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(RoomAddedIntegrationEvent), typeDiscriminator: nameof(RoomAddedIntegrationEvent))]
[JsonDerivedType(typeof(RoomRemovedIntegrationEvent), typeDiscriminator: nameof(RoomRemovedIntegrationEvent))]
[JsonDerivedType(typeof(SessionScheduledIntegrationEvent), typeDiscriminator: nameof(SessionScheduledIntegrationEvent))]
public interface IIntegrationEvent : INotification { }