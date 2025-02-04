using SessionReservation.Application.Common.Interfaces;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ErrorOr<Success>>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IParticipantsRepository _participantsRepository;

    public CreateReservationCommandHandler(ISessionsRepository sessionsRepository, IParticipantsRepository participantsRepository)
    {
        _sessionsRepository = sessionsRepository;
        _participantsRepository = participantsRepository;
    }

    public async Task<ErrorOr<Success>> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var session = await _sessionsRepository.GetByIdAsync(command.SessionId);

        if (session is null)
        {
            return Error.NotFound(description: "Session not found");
        }

        if (session.HasReservationForParticipant(command.ParticipantId))
        {
            return Error.Conflict(description: "Participant already has reservation");
        }

        var participant = await _participantsRepository.GetByIdAsync(command.ParticipantId);

        if (participant is null)
        {
            return Error.NotFound(description: "Participant not found");
        }

        if (participant.HasReservationForSession(session.Id))
        {
            return Error.Unexpected(description: "Participant not expected to have reservation to session");
        }

        if (!participant.IsTimeSlotFree(session.Date, session.Time))
        {
            return Error.Conflict(description: "Participant's calendar is not free for the entire session duration");
        }

        var reserveSpotResult = session.ReserveSpot(participant);

        if (reserveSpotResult.IsError)
        {
            return reserveSpotResult.Errors;
        }

        await _sessionsRepository.UpdateAsync(session);

        return Result.Success;
    }
}
