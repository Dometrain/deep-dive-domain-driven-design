using Ardalis.SmartEnum;

namespace SessionReservation.Domain.SessionAggregate;

public class SessionCategory : SmartEnum<SessionCategory>
{
    public static readonly SessionCategory Kickboxing = new(nameof(Kickboxing), 0);
    public static readonly SessionCategory Functional = new(nameof(Functional), 1);
    public static readonly SessionCategory Zoomba = new(nameof(Zoomba), 2);
    public static readonly SessionCategory Pilates = new(nameof(Pilates), 3);
    public static readonly SessionCategory Yoga = new(nameof(Yoga), 3);

    public SessionCategory(string name, int value) : base(name, value)
    {
    }
}
