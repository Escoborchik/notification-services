namespace Framework.Services.Implementation;

public class TimeProvider : ITimeProvider
{
    public DateTime UtcNow() => DateTime.UtcNow;
}
