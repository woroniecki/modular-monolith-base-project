namespace SharedUtils.Time;

internal sealed class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}
