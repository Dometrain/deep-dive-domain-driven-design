using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.TrainerAggregate;

using MediatR;

using SharedKernel.IntegrationEvents.UserManagement;

namespace SessionReservation.Application.Trainers.IntegrationEvents;

public class TrainerProfileCreatedEventHandler : INotificationHandler<TrainerProfileCreatedIntegrationEvent>
{
    private readonly ITrainersRepository _trainersRepository;

    public TrainerProfileCreatedEventHandler(ITrainersRepository trainersRepository)
    {
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(TrainerProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var trainer = new Trainer(notification.UserId, id: notification.TrainerId);

        await _trainersRepository.AddTrainerAsync(trainer);
    }
}
