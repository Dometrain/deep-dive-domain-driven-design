using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface ITrainersRepository
{
    Task AddTrainerAsync(Trainer participant);
    Task<Trainer?> GetByIdAsync(Guid trainerId);
    Task UpdateAsync(Trainer trainer);
}