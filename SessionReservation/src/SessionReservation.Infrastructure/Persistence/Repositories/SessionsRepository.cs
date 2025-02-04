using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.SessionAggregate;

using Microsoft.EntityFrameworkCore;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class SessionsRepository : ISessionsRepository
{
    private readonly SessionReservationDbContext _dbContext;

    public SessionsRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSessionAsync(Session session)
    {
        await _dbContext.Sessions.AddAsync(session);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Session?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Sessions.FirstOrDefaultAsync(session => session.Id == id);
    }

    public async Task<List<Session>> ListByIds(
        IReadOnlyList<Guid> sessionIds,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        List<SessionCategory>? categories = null)
    {
        return await _dbContext.Sessions
            .AsNoTracking()
            .Where(session => sessionIds.Contains(session.Id))
            .WhereBetweenDateAndTimes(startDateTime, endDateTime)
            .WhereOfCategory(categories)
            .ToListAsync();
    }

    public async Task<List<Session>> ListByGymIdAsync(
        Guid gymId,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        List<SessionCategory>? categories = null)
    {
        var gymRooms = await _dbContext.Rooms
            .AsNoTracking()
            .Where(room => room.GymId == gymId)
            .ToListAsync();

        var sessionIds = gymRooms.SelectMany(room => room.SessionIds).ToList();

        return await ListByIds(sessionIds, startDateTime, endDateTime, categories);
    }

    public async Task UpdateAsync(Session session)
    {
        _dbContext.Update(session);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Session>> ListByRoomId(Guid roomId)
    {
        return await _dbContext.Sessions
            .Where(session => session.RoomId == roomId)
            .ToListAsync();
    }

    public async Task RemoveRangeAsync(List<Session> sessions)
    {
        _dbContext.RemoveRange(sessions);
        await _dbContext.SaveChangesAsync();
    }
}

file static class DbContextSessionExtensions
{
    public static IQueryable<Session> WhereBetweenDateAndTimes(this IQueryable<Session> query, DateTime? start, DateTime? end)
    {
        if (start == null && end == null)
        {
            return query;
        }

        start ??= DateTime.MinValue;
        end ??= DateTime.MaxValue;

        return query
            .AsNoTracking()
            .Where(session => session.Date >= DateOnly.FromDateTime(start.Value))
            .Where(session => session.Date <= DateOnly.FromDateTime(end.Value))
            .Where(session => session.Time.Start >= TimeOnly.FromDateTime(start.Value));
    }

    public static IQueryable<Session> WhereOfCategory(this IQueryable<Session> query, List<SessionCategory>? categories)
    {
        if (categories is null || categories.Count == 0)
        {
            return query;
        }

        var categoryNames = categories.ConvertAll(category => category.Name);

        return query
            .AsNoTracking()
            .Where(session => session.Categories.Any(category => categoryNames.Contains(category.Name)));
    }
}