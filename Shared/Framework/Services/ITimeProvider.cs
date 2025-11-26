namespace Framework.Services
{
    public interface ITimeProvider
    {
        DateTime UtcNow();
    }
}