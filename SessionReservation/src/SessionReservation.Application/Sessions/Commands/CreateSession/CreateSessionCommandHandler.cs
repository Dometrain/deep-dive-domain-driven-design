using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.SessionAggregate;

using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Sessions.Commands.CreateSession;

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, ErrorOr<Session>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ITrainersRepository _trainersRepository;

    public CreateSessionCommandHandler(ITrainersRepository trainersRepository, IRoomsRepository roomsRepository)
    {
        _trainersRepository = trainersRepository;
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<Session>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
    {
        var room = await _roomsRepository.GetByIdAsync(command.RoomId);

        if (room is null)
        {
            return Error.NotFound(description: "Room not found");
        }

        var trainer = await _trainersRepository.GetByIdAsync(command.TrainerId);

        if (trainer is null)
        {
            return Error.NotFound(description: "Trainer not found");
        }

        var createTimeRangeResult = TimeRange.FromDateTimes(command.StartDateTime, command.EndDateTime);

        if (createTimeRangeResult.IsError && createTimeRangeResult.FirstError.Type == ErrorType.Validation)
        {
            return Error.Validation(description: "Invalid date and time");
        }

        if (!trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), createTimeRangeResult.Value))
        {
            return Error.Conflict(description: "Trainer's calendar is not free for the entire session duration");
        }

        var session = new Session(
            name: command.Name,
            description: command.Description,
            maxParticipants: command.MaxParticipants,
            roomId: command.RoomId,
            trainerId: command.TrainerId,
            date: DateOnly.FromDateTime(command.StartDateTime),
            time: createTimeRangeResult.Value,
            categories: command.Categories);

        var scheduleSessionResult = room.ScheduleSession(session);

        if (scheduleSessionResult.IsError)
        {
            return scheduleSessionResult.Errors;
        }

        await _roomsRepository.UpdateAsync(room);

        return session;
    }
}