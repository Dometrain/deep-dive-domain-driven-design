using GymManagement.Domain.Common;

namespace GymManagement.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    public string Name { get; } = null!;

    public Guid GymId { get; }
    public int MaxDailySessions { get; }

    public Room(
        string name,
        Guid gymId,
        int maxDailySessions,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        GymId = gymId;
        MaxDailySessions = maxDailySessions;
    }
}