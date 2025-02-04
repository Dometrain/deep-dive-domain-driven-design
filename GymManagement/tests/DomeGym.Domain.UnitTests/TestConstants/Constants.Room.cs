namespace GymManagement.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Room
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxSessions = Subscriptions.MaxSessionsFreeTier;
        public const string Name = "Room 1";
    }
}