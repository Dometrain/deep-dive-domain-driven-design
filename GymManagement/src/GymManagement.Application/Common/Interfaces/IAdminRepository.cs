using GymManagement.Domain.AdminAggregate;

namespace GymManagement.Application.Common.Interfaces;

public interface IAdminsRepository
{
    Task AddAdminAsync(Admin participant);
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
}