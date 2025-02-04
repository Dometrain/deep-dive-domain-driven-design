using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.EventualConsistency;
using SessionReservation.Domain.SessionAggregate.Events;

using MediatR;

using Throw;

namespace SessionReservation.Application.Participants.Events.ParticipantAddedToSchedule;

public class SessionSpotReservedEventHandler : INotificationHandler<SessionSpotReservedEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public SessionSpotReservedEventHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(SessionSpotReservedEvent notification, CancellationToken cancellationToken)
    {
        var participant = await _participantsRepository.GetByIdAsync(notification.Reservation.ParticipantId);
        participant.ThrowIfNull();

        var updateParticipantScheduleResult = participant.AddToSchedule(notification.Session);

        if (updateParticipantScheduleResult.IsError)
        {
            throw new EventualConsistencyException(
                SessionSpotReservedEvent.ParticipantScheduleUpdateFailed,
                updateParticipantScheduleResult.Errors);
        }

        await _participantsRepository.UpdateAsync(participant);
    }
}