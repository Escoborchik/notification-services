namespace Framework.Validation.Helpers;

public static class ValidationHelpers
{
    public static bool IsValidWorkingHours(TimeOnly open, TimeOnly close)
    {
        return open != close && open < close
            && open.Hour >= 6 && close.Hour <= 24;
    }
}
