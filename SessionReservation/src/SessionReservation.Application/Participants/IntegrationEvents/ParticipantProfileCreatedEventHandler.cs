using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;

using MediatR;

using SharedKernel.IntegrationEvents.UserManagement;

namespace SessionReservation.Application.Participants.IntegrationEvents;

public class ParticipantProfileCreatedEventHandler : INotificationHandler<ParticipantProfileCreatedIntegrationEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public ParticipantProfileCreatedEventHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(ParticipantProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var participant = new Participant(notification.UserId, id: notification.ParticipantId);

        await _participantsRepository.AddParticipantAsync(participant);
    }
}
