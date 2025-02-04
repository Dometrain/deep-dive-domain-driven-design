using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.TrainerAggregate;

using Microsoft.EntityFrameworkCore;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class TrainersRepository : ITrainersRepository
{
    private readonly SessionReservationDbContext _dbContext;

    public TrainersRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTrainerAsync(Trainer trainer)
    {
        await _dbContext.Trainers.AddAsync(trainer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Trainer?> GetByIdAsync(Guid trainerId)
    {
        return await _dbContext.Trainers.FirstOrDefaultAsync(trainer => trainer.Id == trainerId);
    }

    public async Task UpdateAsync(Trainer trainer)
    {
        _dbContext.Trainers.Update(trainer);
        await _dbContext.SaveChangesAsync();
    }
}