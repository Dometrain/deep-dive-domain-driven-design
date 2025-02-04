using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.EventualConsistency;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate.Events;

using MediatR;

namespace SessionReservation.Application.Participants.Events.SessionCanceled;

public class SessionCanceledEventHandler : INotificationHandler<SessionCanceledEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public SessionCanceledEventHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(SessionCanceledEvent notification, CancellationToken cancellationToken)
    {
        List<Participant> participants = await _participantsRepository.ListByIds(notification.Session.GetParticipantIds());

        participants.ForEach(participant =>
        {
            var removeFromScheduleResult = participant.RemoveFromSchedule(notification.Session);
            if (removeFromScheduleResult.IsError)
            {
                throw new EventualConsistencyException(
                    SessionCanceledEvent.ParticipantScheduleUpdateFailed,
                    removeFromScheduleResult.Errors);
            }
        });

        await _participantsRepository.UpdateRangeAsync(participants);
    }
}
