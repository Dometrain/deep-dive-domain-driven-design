using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.AdminAggregate;

using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class AdminsRepository : IAdminsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public AdminsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAdminAsync(Admin admin)
    {
        await _dbContext.Admins.AddAsync(admin);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return _dbContext.Admins.FirstOrDefaultAsync(admin => admin.Id == adminId);
    }

    public async Task UpdateAsync(Admin admin)
    {
        _dbContext.Update(admin);
        await _dbContext.SaveChangesAsync();
    }
}