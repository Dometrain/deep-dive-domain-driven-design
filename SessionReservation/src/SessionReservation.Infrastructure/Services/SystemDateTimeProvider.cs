using SessionReservation.Domain.Common.Interfaces;

namespace SessionReservation.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
