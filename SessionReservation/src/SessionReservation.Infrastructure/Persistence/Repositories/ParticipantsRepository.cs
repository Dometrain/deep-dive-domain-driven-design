using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;

using Microsoft.EntityFrameworkCore;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class ParticipantsRepository : IParticipantsRepository
{
    private readonly SessionReservationDbContext _dbContext;

    public ParticipantsRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddParticipantAsync(Participant participant)
    {
        await _dbContext.Participants.AddAsync(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Participant?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Participants.FirstOrDefaultAsync(participant => participant.Id == id);
    }

    public async Task<List<Participant>> ListByIds(List<Guid> ids)
    {
        return await _dbContext.Participants
            .Where(participant => ids.Contains(participant.Id))
            .ToListAsync();
    }

    public async Task UpdateAsync(Participant participant)
    {
        _dbContext.Update(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(List<Participant> participants)
    {
        _dbContext.UpdateRange(participants);
        await _dbContext.SaveChangesAsync();
    }
}